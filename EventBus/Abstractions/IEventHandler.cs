using EventBus.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions
{
    public interface IEventHandler
    {

    }

    public interface IEventHandler<in TEventArguments> : IEventHandler
        where TEventArguments : EventArguments
    {
        Task Handle(TEventArguments @event);
    }
}
