using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.TryAddTransient<IProductDataAccess, ProductDataAccess>();
            services.TryAddTransient<IProductBusiness, ProductBusiness>();
            //services.AddSwaggerExtension();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Product API", Version = "v1" });
            });

            var productCatalogDBConnectionString = Configuration["connectionStrings:ProductCatalogDBConnectionString"];
            services.AddDbContext<ProductDBContext>(x => x.UseSqlServer(productCatalogDBConnectionString));

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
