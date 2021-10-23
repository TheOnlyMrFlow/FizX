using System;

namespace FizX.Core.Events
{
    public interface ISubscription<out TEvent> : IDisposable where TEvent : Event
    {
        public Type GetEventType();
    }
}