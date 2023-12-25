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
            if (timeLine.IsRewinding)
            {
                if (!timeLine.PastStates.TryPop(out var pastState))
                {
                    timeLine.StopRewinding();
                    continue;
                }
                foreach (var actorPastState in pastState.ActorsPastStates.Values)
                {
                    actorPastState.Actor.SetTransform(actorPastState.ActorTransform);
                }

                continue;
            }
            
            foreach (var actor in timeLine.Actors)
            {
                actor.Tick(frame);
                if (timeLine.IsRecording)
                    timeLine.SaveActorStateAtFrame(actor, frame);
            }
        }
    }

    public void AddActor(Actor actor, TimeLineIndex timeLineIndex = TimeLineIndex.TimeLine0)
    {
        Time.GetTimeLine(timeLineIndex).AddActor(actor);
    }
}