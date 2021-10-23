using Xunit;
using FluentAssertions;

using System.Linq;
using FizX.Core.Actors;
using FizX.Core.Actors.ActorComponents;

namespace FizX.Core.Test
{
    public class ActorsTest
    {
        [Fact]
        public void AnActorShouldHaveNoComponentByDefault()
        {
            new Actor().Components.Should().HaveCount(0);
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
    }

    internal class SampleActorComponent : ActorComponent
    {
        
    }
}
