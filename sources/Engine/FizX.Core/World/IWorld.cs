using System.Collections.Generic;
using FizX.Core.Actors;

namespace FizX.Core.World;

public interface IWorld
{
    IEnumerable<IActor> Actors { get; }
    void Tick(int deltaMs);
}