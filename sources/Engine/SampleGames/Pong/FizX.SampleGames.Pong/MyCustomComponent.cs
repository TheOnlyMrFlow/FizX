using System.Numerics;
using FizX.Core;
using FizX.Core.Actors;
using FizX.Core.Actors.ActorComponents;
using FizX.Core.Geometry;
using FizX.Core.Timing;

namespace FizX.SampleGames.Pong;

public class MyCustomComponent : ActorComponent
{
    public override void Tick(FrameInfo frame)
    {
        var timeLine = Time.GetTimeLine(TimeLineIndex.TimeLine0); 
        if (!timeLine.IsRecording)
        {
            timeLine.StartRecording();
        }

        if (frame.Index > 300)
        {
            timeLine.StartRewinding();
        }
        
        Actor!.SetPosition(Actor.Position + Vector2.UnitX);
    }
}