using EventBus.EventArgs;
using EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus
{
    public interface ISubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;

        void AddSubscription<T, TH>()
            where T : EventArguments
            where TH : IEventHandler<T>;

        void RemoveSubscription<T, TH>()
            where T : EventArguments
            where TH : IEventHandler<T>;

        bool HasSubscriptionsForEvent<T>() where T : EventArguments;

        bool HasSubscriptionsForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        void Clear();

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : EventArguments;

        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        string GetEventKey<T>();

    }
}
