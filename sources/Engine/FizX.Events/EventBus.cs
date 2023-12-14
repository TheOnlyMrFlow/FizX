using System;
using System.Collections.Generic;
using FizX.Core.Events;

namespace FizX.Events;

public class EventBus : IEventBus
{
    private readonly IDictionary<Type, ICollection<ISubscriptionImpl<Event>>> _subscriptions = new Dictionary<Type, ICollection<ISubscriptionImpl<Event>>>();

    public void BroadcastEvent<TEvent>(TEvent evt) where TEvent : Event
    {
        if (!_subscriptions.ContainsKey(typeof(TEvent))) return;

        foreach (var subscription in _subscriptions[typeof(TEvent)])
            subscription.InvokeCallback(evt);
    }

    public ISubscription<TEvent> Subscribe<TEvent>(Action<TEvent> callback) where TEvent : Event
    {
        var subscription = new Subscription<TEvent>(callback, OnSubscriptionDispose);
            
        if (! _subscriptions.ContainsKey(typeof(TEvent)))
            _subscriptions.Add(typeof(TEvent), new List<ISubscriptionImpl<Event>>());
            
        _subscriptions[typeof(TEvent)].Add(subscription);

        return subscription;
    }

    public void OnSubscriptionDispose<TEvent>(Subscription<TEvent> subscription) where TEvent : Event
    {
        if (!_subscriptions.ContainsKey(typeof(TEvent))) return;

        _subscriptions[typeof(TEvent)].Remove(subscription);
    }
}