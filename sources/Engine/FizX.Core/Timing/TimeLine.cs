using System;
using System.Collections.Generic;
using FizX.Core.Actors;
using FizX.Core.Exceptions;
using FizX.Core.Geometry;

namespace FizX.Core.Timing;

public class TimeLine
{
    public Stack<TimeLinePastState> _pastStates = new Stack<TimeLinePastState>();
    
    public float TimeScale { get; private set; } = 1f;
    
    public bool RewindEnabled { get; private set; } = false;

    public void EnableRewind() => RewindEnabled = true;
    
    public void SetTimeScale(float scale)
    {
        if (scale < 0f && !RewindEnabled)
        {
            throw new FizXRuntimeException();
        }
        
        TimeScale = scale;
    }

    public void SaveActorStateAtFrame(Actor actor, FrameInfo frame)
    {
        if (!RewindEnabled)
        {
            throw new FizXRuntimeException();
        }

        var lastPastState = _pastStates.Peek();
        if (lastPastState.Frame.Index != frame.Index)
        {
            lastPastState = new TimeLinePastState()
            {
                Frame = frame
            };
            _pastStates.Push(lastPastState);
        }
        
        lastPastState.AddActor(actor);
    }
}

public class TimeLinePastState
{
    public FrameInfo Frame { get; set; }

    public Dictionary<int, ActorStateSnapshot> ActorsPastStates { get; set; } = new Dictionary<int, ActorStateSnapshot>();

    public void AddActor(Actor actor)
    {
        ActorsPastStates[actor.Id] = new ActorStateSnapshot()
        {
            ActorId = actor.Id,
            ActorTransform = actor.Transform
        };
    }
}

public class ActorStateSnapshot
{
    public int ActorId { get; set; }
    
    public Transform ActorTransform { get; set; }
}