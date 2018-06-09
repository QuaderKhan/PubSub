using EventBus.EventArgs;

namespace EventBus.Abstractions
{
    public interface IEventBus
    {
        void Publish(EventArguments @event);

        void Subscribe<T, TH>()
            where T : EventArguments
            where TH : IEventHandler<T>;

        void UnSubscribe<T, TH>()
           where T : EventArguments
           where TH : IEventHandler<T>;
    }
}
