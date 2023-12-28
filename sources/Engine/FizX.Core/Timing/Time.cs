using System.Collections.Generic;
using System.Linq;
using FizX.Core.Actors;

namespace FizX.Core.Timing;

public enum TimeLineIndex
{
    TimeLine0 = 0,
    TimeLine1 = 1,
    TimeLine2 = 2,
    TimeLine3 = 3,
    TimeLine4 = 4,
    TimeLine5 = 5,
    TimeLine6 = 6,
    TimeLine7 = 7,
    TimeLine8 = 8,
    TimeLine9 = 9
}

public static class Time
{
    private static readonly TimeLine[] TimeLines = Enumerable.Range((int)TimeLineIndex.TimeLine0, (int)TimeLineIndex.TimeLine9 + 1).Select(i => new TimeLine((TimeLineIndex)i)).ToArray();

    public static TimeLine GetTimeLine(TimeLineIndex index) => TimeLines[(int)index];

    public static IEnumerable<TimeLine> GetAllTimeLines()
    {
        for (var i = TimeLineIndex.TimeLine0; i <= TimeLineIndex.TimeLine9; i++)
        {
            yield return GetTimeLine(i);
        }
    }

    public static void MoveActorTo(Actor actor, TimeLineIndex timeLineIndex)
    {
        GetTimeLine(actor.TimeLineIndex).Actors.Remove(actor);
        GetTimeLine(timeLineIndex).Actors.Add(actor);
        actor.TimeLineIndex = timeLineIndex;
    }
}