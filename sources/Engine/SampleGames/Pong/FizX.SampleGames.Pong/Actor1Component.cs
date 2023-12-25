using System.Numerics;
using FizX.Core;
using FizX.Core.Actors;
using FizX.Core.Actors.ActorComponents;
using FizX.Core.Geometry;
using FizX.Core.Timing;

namespace FizX.SampleGames.Pong;

public class Actor1Component : ActorComponent
{
    public override void Tick(FrameInfo frame)
    {
        var timeLine = Time.GetTimeLine(TimeLineIndex.TimeLine1);
        if (!timeLine.IsRecording)
        {
            timeLine.StartRecording();
        }
        
        if (Actor!.Transform.Position.X > 75)
        {
            timeLine.StartRewinding();
        }

        if (Actor.Position.Y < 200)
        {
            Actor!.SetPosition(Actor.Position + (frame.DeltaTimeMs / 1000f) * 10f * Vector2.UnitY);
        }
        
        Actor!.SetPosition(Actor.Position + (frame.DeltaTimeMs / 1000f) * 10f * Vector2.UnitX);
    }
}