using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
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

    public FrameInfo? StartedRecordingAtFrame => PastStates.LastOrDefault()?.Frame;

    private float _recordingHoleMS = 0f;
    
    private float _rewindedTimeMs = 0f;

    public float GetTotalRecordingTimeMs()
    {
        if (PastStates.Count == 0 || StartedRecordingAtFrame == null)
            return 0f;
        
        return PastStates.Peek().Frame.ElapsedMs - StartedRecordingAtFrame.Value.ElapsedMs - _recordingHoleMS;
    }

    public FrameInfo? StartedRewindingAtFrame { get; private set; } = null;

    public bool IsRewinding => StartedRewindingAtFrame != null;

    public float AheadMs { get; internal set; } = 0f;

    public void StartRewinding(FrameInfo frame) => StartedRewindingAtFrame = frame;

    public bool TryRewindFrame(out TimeLinePastState pastState)
    {
        if (!PastStates.TryPop(out pastState)) 
            return false;
        
        _rewindedTimeMs += pastState.Frame.DeltaTimeMs;
        return true;
    }

    // // todo: optim
    // public float GetRewindProgressOld()
    // {
    //     if (!IsRewinding)
    //         return 0f;
    //     
    //     if (PastStates.Count == 0)
    //         return 1f;
    //
    //     var totalFramesToRewind = StartedRewindingAtFrame!.Value.Index - PastStates.Last().Frame.Index;
    //     var currentFrameIndexRelativeToEndOfRewind = PastStates.Peek().Frame.Index - PastStates.Last().Frame.Index;
    //     
    //     return 1f - (float) currentFrameIndexRelativeToEndOfRewind / totalFramesToRewind;
    // }

    public float GetRewindProgress()
    {
        return Math.Min(1, _rewindedTimeMs / GetTotalRecordingTimeMs());
    }

    public void StopRewinding()
    {
        StartedRewindingAtFrame = null;
        _rewindedTimeMs = 0f;
    }

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

        TimeLinePastState? lastPastState = null;
        PastStates.TryPeek(out lastPastState);
        
        if (lastPastState != null && lastPastState.Frame.Index != frame.Index - 1)
        {
            _recordingHoleMS += frame.ElapsedMs - lastPastState.Frame.ElapsedMs;
        }

        if (lastPastState == null || lastPastState.Frame.Index != frame.Index)
        {
            lastPastState = new TimeLinePastState()
            {
                Frame = frame
            };
            PastStates.Push(lastPastState);
        }
        
        lastPastState.AddActor(actor);
    }

    public void Tick(FrameInfo frame)
    {
        if (!IsRewinding)
        {
            foreach (var actor in Actors)
            {
                actor.Tick(frame);
                if (IsRecording)
                    SaveActorStateAtFrame(actor, frame);
            }
                
            return;
        }

        AheadMs -= frame.DeltaTimeMs;
        while (AheadMs <= 0f)
        {
            if (!TryRewindFrame(out var pastState))
            {
                StopRewinding();

                foreach (var actor in Actors)
                {
                    actor.OnStopRewinding();
                }

                return;
            }

            AheadMs += pastState.Frame.DeltaTimeMs / TimeScale;
            foreach (var actorPastState in pastState.ActorsPastStates.Values)
            {
                if (pastState.Frame.Index == frame.Index - 1)
                {
                    actorPastState.Actor.OnStartRewinding();
                }
                else
                {
                    actorPastState.Actor.RewindTick();
                }

                actorPastState.Actor.SetTransform(actorPastState.ActorTransform);
            }
        }
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