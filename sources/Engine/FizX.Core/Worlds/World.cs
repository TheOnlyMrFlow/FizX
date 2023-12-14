using System.Collections.Generic;
using FizX.Core.Actors;

namespace FizX.Core.Worlds;

public class World : IWorld
{
    private readonly List<Actor> _actors = new List<Actor>();
    public IEnumerable<Actor> Actors => _actors;

    public void Tick(int deltaTime)
    {
        foreach (var actor in _actors)
            actor.Tick(deltaTime);
    }

    public void AddActor(Actor actor)
    {
        _actors.Add(actor);
    }
}