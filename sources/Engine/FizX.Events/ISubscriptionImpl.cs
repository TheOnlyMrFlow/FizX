using FizX.Core.Events;

namespace FizX.Events
{
    public interface ISubscriptionImpl<out TEvent> : ISubscription<TEvent> where TEvent : Event
    {
        void InvokeCallback(Event evt);
    }
}