using Xunit;
using Moq;
using FluentAssertions;

using System.Linq;
using System.Collections.Generic;
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
        public void GivenAComponentWasAdded_ThenActorShouldHaveTheAddedComponent()
        {
            var actor = new Actor();
            var component = new ActorComponent();

            actor.AttachComponent(component);

            actor.Components.Should().HaveCount(1);
            actor.Components.FirstOrDefault().Should().Be(component);
        }
        
        [Fact]
        public void GivenAComponentWasAddedToActor_TheComponentActorShouldBeTheActorItWasAddedTo()
        {
            var actor = new Actor();
            var component = new ActorComponent();

            actor.AttachComponent(component);

            component.GetActor().Should().Be(actor);
        }
        
        [Fact]
        public void GivenAComponentWasRemoved_ThenActorShouldNotHaveTheRemovedComponent()
        {
            var actor = new Actor();
            var component = new ActorComponent();
            actor.AttachComponent(component);
            actor.RemoveComponent(component);
            
            actor.Components.Should().HaveCount(0);
        }
    }
}
