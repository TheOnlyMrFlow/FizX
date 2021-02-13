using Xunit;
using Moq;
using FizX.Core.Rendering;
using FizX.Core.Physics;
using FizX.Core.Logging;
using FizX.Core.Input;
using FizX.Core.ContentLoading;
using FluentAssertions;

using System.Linq;
using System.Collections.Generic;

namespace FizX.Core.Test
{
    public class WorldTest
    {
        private World _world = new World();

        private IEnumerable<Mock<IActor>> _actorsMocks;
        public WorldTest()
        {
            _actorsMocks = Enumerable.Range(0, 3).Select(i =>
            {
                var mock = new Mock<IActor>();
                mock.Setup(obj => obj.Id).Returns(i);

                return mock;
            }).ToList();
        }

        [Fact]
        public void World_ShouldHaveNoActor_UponCreation()
        {
            new World().Actors.Should().BeEmpty();
        }

        [Fact]
        public void GettingActors_ShouldReturnAddedActors()
        {
            var actor1 = _actorsMocks.First().Object;
            var actor2 = _actorsMocks.ElementAt(1).Object;
            _world.AddActor(actor1);
            _world.AddActor(actor2);

            _world.Actors.Should().BeEquivalentTo(actor1, actor2);
        }

        [Fact]
        public void Tick_ShouldCallTickOnEveryActors()
        {
            _world.AddActor(_actorsMocks.First().Object);
            _world.AddActor(_actorsMocks.ElementAt(1).Object);

            _world.Tick(10);

            _actorsMocks.First().Verify(obj => obj.Tick(10), Times.Once);
            _actorsMocks.ElementAt(1).Verify(obj => obj.Tick(10), Times.Once);
        }
    }
}
