using System;

namespace FizX.Core.Events
{
    public interface IEventBus
    {
        public void BroadcastEvent<TEvent>(TEvent @event) where TEvent: Event;

        public ISubscription<TEvent> Subscribe<TEvent>(Action<TEvent> callback) where TEvent : Event;
    }
}