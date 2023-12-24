using System.Collections.Generic;
using FizX.Core.Actors;
using FizX.Core.Timing;

namespace FizX.Core.Worlds;

public class World : IWorld
{
    private readonly List<Actor> _actors = new List<Actor>();
    public IEnumerable<Actor> Actors => _actors;

    public void Tick(FrameInfo frame)
    {
        foreach (var actor in _actors)
        {
            var timeLine = Time.GetTimeLine(actor.TimeLineIndex);
            actor.Tick(frame.DeltaTime * timeLine.TimeScale);
            if (timeLine.RewindEnabled)
                timeLine.SaveActorStateAtFrame(actor, frame);
        }

        for (var i = TimeLineIndex.TimeLine0; i < TimeLineIndex.TimeLine9; i++)
        {
            var timeLine = Time.GetTimeLine(0);
            if (timeLine.RewindEnabled)
            {
                timeLine.
            }
        }
    }

    public void AddActor(Actor actor)
    {
        _actors.Add(actor);
    }
}