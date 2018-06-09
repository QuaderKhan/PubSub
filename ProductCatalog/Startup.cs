using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductCatalog.Business;
using ProductCatalog.DataAccess;
using ProductCatalog.DataAccess.EntityFramework;
using ProductCatalog.DataAccess.Interface;
using ProductCatalog.EventHandling;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Swagger;

namespace PubSubCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var subscriptionClientName = Configuration["SubscriptionClientName"];
            services.AddMvc();
            services.TryAddTransient<IProductDataAccess, ProductDataAccess>();
            services.TryAddTransient<IProductBusiness, ProductBusiness>();
            services.AddTransient<IEventPublishService, EventPublishService>();

            if (Configuration.GetValue<string>("BrokerName").Equals("rabbitmq", StringComparison.OrdinalIgnoreCase))
            {
                //Add dependency of RabbitMQ connection class (DefaultRabbitMQPersistentConnection) to the container
                services.AddTransient<IRabbitMQPersisterConnection>(srvp =>
                {
                    var logger = srvp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                    var factory = new ConnectionFactory()
                    {
                        HostName = Configuration["EventBusConnection"]
                    };

                    if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
                    {
                        factory.UserName = Configuration["EventBusUserName"];
                    }

                    if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
                    {
                        factory.Password = Configuration["EventBusPassword"];
                    }

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                    {
                        retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                    }

                    return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
                });

                //Add dependency of RabbitMQ class (EventBusRabbitMQ) to the container
                services.AddTransient<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(srvp =>
                {
                    var rabbitMQPersistenConnection = srvp.GetRequiredService<IRabbitMQPersisterConnection>();
                    var logger = srvp.GetRequiredService<ILogger<IEventBus>>();
                    var lifetimeScope = srvp.GetRequiredService<ILifetimeScope>();
                    var subscriptionManager = srvp.GetRequiredService<ISubscriptionsManager>();
                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                    {
                        retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                    }
                    return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistenConnection, logger, lifetimeScope, subscriptionManager, subscriptionClientName, retryCount);
                });
            }
            else if (Configuration.GetValue<string>("BrokerName").Equals("kafka", StringComparison.OrdinalIgnoreCase))
            {

            }

            //Add dependency of SubscriptionManager class (InMemoryEventSubscriptionManager) to the container
            services.AddSingleton<ISubscriptionsManager, InMemoryEventSubscriptionManager>();

            //services.AddSwaggerExtension();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Product API", Version = "v1" });
            });

            var productCatalogDBConnectionString = Configuration["connectionStrings:ProductCatalogDBConnectionString"];
            services.AddDbContext<ProductDBContext>(x => x.UseSqlServer(productCatalogDBConnectionString));

            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
            });

        }
    }
}
