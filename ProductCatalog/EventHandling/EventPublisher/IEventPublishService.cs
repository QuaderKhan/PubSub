using EventBus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.EventHandling
{
    public interface IEventPublishService
    {
        Task PublishThroughEventBusAsync(EventArguments eventArgs);
    }
}
