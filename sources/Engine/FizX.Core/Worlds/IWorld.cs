using System.Collections.Generic;
using FizX.Core.Actors;

namespace FizX.Core.Worlds;

public interface IWorld
{
    IEnumerable<Actor> Actors { get; }
    void Tick(int deltaMs);
}