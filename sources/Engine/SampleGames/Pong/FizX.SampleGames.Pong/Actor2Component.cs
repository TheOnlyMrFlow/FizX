using System.Numerics;
using FizX.Core;
using FizX.Core.Actors;
using FizX.Core.Actors.ActorComponents;
using FizX.Core.Geometry;
using FizX.Core.Timing;

namespace FizX.SampleGames.Pong;

public class Actor2Component : ActorComponent
{
    public override void Tick(FrameInfo frame)
    {
        var timeLine = Time.GetTimeLine(TimeLineIndex.TimeLine0);
        if (!timeLine.IsRecording)
        {
            timeLine.StartRecording();
        }
        
        if (Actor!.Transform.Position.X > 50)
        {
            timeLine.StartRewinding(frame);
        }
        
        Actor!.SetPosition(Actor.Position + (frame.DeltaTimeMs / 1000f) * 10f * Vector2.UnitX);
    }
}