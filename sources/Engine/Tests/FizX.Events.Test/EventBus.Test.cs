using System;
using FizX.Core.Events;
using FluentAssertions;
using Xunit;

namespace FizX.Events.Test;

public class EventBusTest
{
    private readonly EventBus _eventBus = new();
            
    [Fact]
    public void SubscribingToSampleEvent_ShouldReturnASubscriptionOfGenericTypeSampleEvent()
    {
        var subscription = _eventBus.Subscribe<SampleEvent>((e) => { });
        subscription.GetEventType().Should().Be(typeof(SampleEvent));
    }
        
    [Fact]
    public void BroadcastingAnEvent_ShouldCallSubscriptionCallbackWithTheGivenEventAsParameter()
    {
        var eventData = string.Empty;
        _eventBus.Subscribe<SampleEvent>((e) => { eventData = e.SampleData; });
            
        eventData.Should().BeEmpty();
            
        _eventBus.BroadcastEvent(new SampleEvent {SampleData = "toto"});
        eventData.Should().Be("toto");
    }
        
    [Fact]
    public void DisposingASubscription_ShouldPreventTheCallbackFromBeingCalled()
    {
        var eventData = string.Empty;
            
        var subscription = _eventBus.Subscribe<SampleEvent>((e) => { eventData = e.SampleData; });
        subscription.Dispose();

        _eventBus.BroadcastEvent(new SampleEvent {SampleData = "toto"});
        eventData.Should().Be(string.Empty);
    }
}
    
public class SampleEvent : Event
{
    public string SampleData { get; set; }
}