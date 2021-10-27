using System.Linq;
using FizX.Core.Actors;
using FizX.Core.Actors.ActorComponents;
using FluentAssertions;
using Xunit;

namespace FizX.Core.Test.Actors
{
    public class Actor_Test
    {
        [Fact]
        public void AnActorShouldHaveNoComponentByDefault()
        {
            new Actor().Components.Should().BeEmpty();
        }
        
        [Fact]
        public void AnActorComponentShouldHaveNoActorByDefault()
        {
            new SampleActorComponent().GetActor().Should().BeNull();
        }
        
        [Fact]
        public void GivenAComponentWasAdded_ThenActorShouldHaveTheAddedComponent()
        {
            var actor = new Actor();
            var component = new SampleActorComponent();

            actor.AttachComponent(component);

            actor.Components.Should().HaveCount(1);
            actor.Components.FirstOrDefault().Should().Be(component);
        }
        
        [Fact]
        public void GivenAComponentWasAddedToActor_TheComponentActorShouldBeTheActorItWasAddedTo()
        {
            var actor = new Actor();
            var component = new SampleActorComponent();

            actor.AttachComponent(component);

            component.GetActor().Should().Be(actor);
        }
        
        [Fact]
        public void GivenAComponentWasRemoved_ThenActorShouldNotHaveTheRemovedComponent()
        {
            var actor = new Actor();
            var component = new SampleActorComponent();
            actor.AttachComponent(component);
            actor.RemoveComponent(component);
            
            actor.Components.Should().HaveCount(0);
        }
        
        [Fact]
        public void GivenAComponentWasRemovedFromActor_TheComponentActorShouldBeNull()
        {
            var actor = new Actor();
            var component = new SampleActorComponent();

            actor.AttachComponent(component);
            actor.RemoveComponent(component);

            component.GetActor().Should().BeNull();
        }
        
        [Fact]
        public void CallingTickOnActorShouldCallTickOnEveryOfItsComponents()
        {
            var actor = new Actor();
            var component1 = new SampleActorComponent();
            var component2 = new SampleActorComponent();
            actor.AttachComponent(component1);
            actor.AttachComponent(component2);
            
            actor.Tick(12);
            component1.TickCount.Should().Be(1);
            component2.TickCount.Should().Be(1);
            component1.LastTickDeltaMs.Should().Be(12);
            component2.LastTickDeltaMs.Should().Be(12);
            
            actor.Tick(16);
            component1.TickCount.Should().Be(2);
            component2.TickCount.Should().Be(2);
            component1.LastTickDeltaMs.Should().Be(16);
            component2.LastTickDeltaMs.Should().Be(16);
        }
    }

    internal class SampleActorComponent : ActorComponent
    {
        public int TickCount { get; private set; } = 0;
        public int LastTickDeltaMs { get; private set; } = 0;

        public override void Tick(int deltaMs)
        {
            TickCount++;
            LastTickDeltaMs = deltaMs;
        }
    }
}
