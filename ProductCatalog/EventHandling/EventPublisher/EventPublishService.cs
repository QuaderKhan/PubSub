using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.EventArgs;

namespace ProductCatalog.EventHandling
{
    public class EventPublishService : IEventPublishService
    {
        private readonly IEventBus eventBus;
        public EventPublishService(IEventBus EventBus)
        {
            eventBus = EventBus;
        }

        public async Task PublishThroughEventBusAsync(EventArguments eventArgs)
        {
            eventBus.Publish(eventArgs);
        }
    }
}
