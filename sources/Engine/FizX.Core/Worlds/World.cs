using System;
using System.Collections.Generic;
using System.Linq;
using FizX.Core.Actors;
using FizX.Core.Timing;

namespace FizX.Core.Worlds;

public class World : IWorld
{
    public IEnumerable<Actor> Actors => Time.GetAllTimeLines().SelectMany(t => t.Actors);

    public void Tick(FrameInfo frame)
    {
        foreach (var timeLine in Time.GetAllTimeLines())
        {
            if (!timeLine.IsRewinding)
            {
                foreach (var actor in timeLine.Actors)
                {
                    actor.Tick(frame);
                    if (timeLine.IsRecording)
                        timeLine.SaveActorStateAtFrame(actor, frame);
                }
                
                continue;
            }

            timeLine.AheadMs -= frame.DeltaTimeMs;
            while (timeLine.AheadMs <= 0f)
            {
                if (!timeLine.PastStates.TryPop(out var pastState))
                {
                    timeLine.StopRewinding();

                    foreach (var actor in timeLine.Actors)
                    {
                        actor.OnStopRewinding();
                    }

                    return;
                }

                timeLine.AheadMs += pastState.Frame.DeltaTimeMs / timeLine.TimeScale;
                foreach (var actorPastState in pastState.ActorsPastStates.Values)
                {
                    if (pastState.Frame.Index == frame.Index - 1)
                    {
                        actorPastState.Actor.OnStartRewinding();
                    }

                    actorPastState.Actor.SetTransform(actorPastState.ActorTransform);
                    actorPastState.Actor.RewindTick();
                }
            }
        }
    }

    public void AddActor(Actor actor, TimeLineIndex timeLineIndex = TimeLineIndex.TimeLine0)
    {
        Time.MoveActorTo(actor, timeLineIndex);
    }
}