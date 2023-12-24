using System;
using System.Collections.Generic;
using FizX.Core.Actors;
using FizX.Core.Exceptions;
using FizX.Core.Geometry;

namespace FizX.Core.Timing;

public class TimeLine
{
    public Stack<TimeLinePastState> _pastStates = new Stack<TimeLinePastState>();
    
    private readonly List<Actor> _actors = new List<Actor>();
    public IEnumerable<Actor> Actors => _actors;

    public void AddActor(Actor actor) => _actors.Add(actor);
    
    public float TimeScale { get; private set; } = 1f;
    
    public bool IsRecording { get; private set; } = false;

    public void StartRecording() => IsRecording = true;
    
    public bool IsRewinding { get; private set; } = false;

    public void StartRewinding() => IsRewinding = true;
    
    public void SetTimeScale(float scale)
    {
        if (scale < 0f && !IsRecording)
        {
            throw new FizXRuntimeException();
        }
        
        TimeScale = scale;
    }

    public void SaveActorStateAtFrame(Actor actor, FrameInfo frame)
    {
        if (!IsRecording)
        {
            throw new FizXRuntimeException();
        }

        if (!_pastStates.TryPeek(out var lastPastState) || lastPastState.Frame.Index != frame.Index)
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
            Actor = actor,
            ActorTransform = actor.Transform
        };
    }
}

public class ActorStateSnapshot
{
    public Actor Actor { get; set; }
    
    public Transform ActorTransform { get; set; }
}