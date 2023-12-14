using System.Numerics;
using FizX.Core.Actors;
using FizX.Core.Actors.ActorComponents;
using FizX.Core.Geometry;

namespace FizX.SampleGames.Pong;

public class MyCustomComponent : ActorComponent
{
    public override void Tick(int deltaMs)
    {
        Actor!.SetPosition(Actor.Position + Vector2.UnitX);
    }
}