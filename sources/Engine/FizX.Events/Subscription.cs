using System;
using FizX.Core;
using FizX.Core.Events;
using FizX.Core.Exceptions;

namespace FizX.Events;

public class Subscription<TEvent> : ISubscriptionImpl<TEvent> where TEvent : Event
{
    private readonly Action<Subscription<TEvent>> _onDispose;
    public Type GetEventType() => typeof(TEvent);

    private Action<TEvent> CallBack { get;}

    public void InvokeCallback(Event evt)
    {
        if (evt is not TEvent castedEvt)
            throw new FizXRuntimeException();

        InvokeCallback(castedEvt);
    }

    private void InvokeCallback(TEvent evt) => CallBack(evt);

    public Subscription(Action<TEvent> callback, Action<Subscription<TEvent>> onDispose)
    {
        _onDispose = onDispose;
        CallBack = callback;
    }

    public void Dispose() => _onDispose(this);
}