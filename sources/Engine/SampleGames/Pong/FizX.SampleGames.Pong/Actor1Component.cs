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
        if (!TimeLine.IsRecording)
        {
            TimeLine.StartRecording();
        }
        
        // if (Actor!.Transform.Position.X > 75)
        // {
        //     timeLine.StartRewinding();
        // }

        if (Game.Instance.InputManager.GetInputVector("Rewind") != Vector2.Zero)
        {
            TimeLine.StartRewinding(frame);
        }

        // if (Actor.Position.Y < 200)
        // {
        //     Actor!.SetPosition(Actor.Position + (frame.DeltaTimeMs / 1000f) * 10f * Vector2.UnitY);
        // }
        
        
        Actor!.SetPosition(Actor.Position + (frame.DeltaTimeMs / 1000f) * 50f * Game.InputManager.GetInputVector("Move"));
        
        
        //Thread.Sleep(500);
    }

    public override void RewindTick()
    {
        TimeLine.SetTimeScale(Math.Min(10f, TimeLine.TimeScale * 1.01f));
    }

    public override void OnStartRewinding()
    {
        //var timeLine = Time.GetTimeLine(TimeLineIndex.TimeLine1);
        //timeLine.SetTimeScale(5f);
    }
    
    public override void OnStopRewinding()
    {
        TimeLine.SetTimeScale(1f);
    }
}