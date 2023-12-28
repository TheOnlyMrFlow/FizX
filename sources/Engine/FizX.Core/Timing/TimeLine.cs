using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using FizX.Core.Actors;
using FizX.Core.Exceptions;
using FizX.Core.Geometry;

namespace FizX.Core.Timing;

public class TimeLine
{
    public TimeLine(TimeLineIndex index)
    {
        Index = index;
    }
    
    public TimeLineIndex Index { get; }
    
    public readonly Stack<TimeLinePastState> PastStates = new Stack<TimeLinePastState>();
    
    internal readonly HashSet<Actor> Actors = [];
    public ImmutableHashSet<Actor> GetActors() => Actors.ToImmutableHashSet();

    public float TimeScale { get; private set; } = 1f;
    
    public bool IsRecording { get; private set; } = false;

    public void StartRecording() => IsRecording = true;

    public float StartedRewindingAt { get; private set; } = -1f;

    public bool IsRewinding => StartedRewindingAt >= 0f;

    public float AheadMs { get; internal set; } = 0f;

    public void StartRewinding(FrameInfo frame) => StartedRewindingAt = frame.ElapsedMs;

    public void StopRewinding() => StartedRewindingAt = -1f;
    
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
            throw new FizXRuntimeException("Timeline not recording");
        }
        
        if (!Actors.Contains(actor))
        {
            throw new FizXRuntimeException("Actor is not part of this timeline");
        }

        if (!PastStates.TryPeek(out var lastPastState) || lastPastState.Frame.Index != frame.Index)
        {
            lastPastState = new TimeLinePastState()
            {
                Frame = frame
            };
            PastStates.Push(lastPastState);
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