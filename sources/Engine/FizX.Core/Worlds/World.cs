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
            timeLine.Tick(frame);
        }
    }

    public void AddActor(Actor actor, TimeLineIndex timeLineIndex = TimeLineIndex.TimeLine0)
    {
        Time.MoveActorTo(actor, timeLineIndex);
    }
}