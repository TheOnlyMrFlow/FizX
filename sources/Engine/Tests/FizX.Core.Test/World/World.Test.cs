using System.Collections.Generic;
using System.Linq;
using FizX.Core.Actors;
using FluentAssertions;
using Moq;
using Xunit;

namespace FizX.Core.Test.World;

public class World_Test
{
    private readonly Core.World.World _world = new();

    private readonly IEnumerable<Mock<IActor>> _actorsMocks;
    public World_Test()
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
        new Core.World.World().Actors.Should().BeEmpty();
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