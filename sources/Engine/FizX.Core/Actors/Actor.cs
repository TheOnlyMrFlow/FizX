using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using FizX.Core.Actors.ActorComponents;
using FizX.Core.Geometry;
using FizX.Core.Timing;

namespace FizX.Core.Actors;

public class Actor
{
    private static int _lastRandomId = -1;
    
    public static int NextRandomId() => Interlocked.Increment(ref _lastRandomId);
    
    public readonly int Id = NextRandomId();

    public TimeLineIndex TimeLineIndex { get; internal set; } = TimeLineIndex.TimeLine0;

    public TimeLine TimeLine => Time.GetTimeLine(TimeLineIndex);

    public Transform Transform { get; private set; } = new();

    public Vector2 Position => Transform.Position;

    public float Rotation => Transform.Rotation;

    private readonly ICollection<ActorComponent> _components = new List<ActorComponent>();

    public IEnumerable<ActorComponent> Components => _components;

    public void SetTransform(Transform transform) => Transform = transform;
    
    public void SetPosition(Vector2 position) => Transform = new Transform(position, Transform.Rotation);
    
    public void SetRotation(float rotation) => Transform = new Transform(Transform.Position, rotation);
    
    public void AttachComponent(ActorComponent component)
    {
        component.Actor = this;
        _components.Add(component);
    }

    public void RemoveComponent(ActorComponent component)
    {
        _components.Remove(component);
        component.Actor = null;
    }

    internal void Tick(FrameInfo frame)
    {
        foreach (var component in _components) 
            component.Tick(frame);
    }
    
    public void RewindTick()
    {
        foreach (var component in _components) 
            component.RewindTick();
    }

    public void OnStartRewinding()
    {
        foreach (var component in _components) 
            component.OnStartRewinding();
    }

    public void OnStopRewinding()
    {
        foreach (var component in _components) 
            component.OnStopRewinding();
    }
    
    internal void MoveToTimeline(TimeLineIndex timeLineIndex)
    {
        Time.MoveActorTo(this, timeLineIndex);
    }

    public override string ToString()
    {
        return $"[Actor:{Id}] [Transform:{Transform}] [Components: {string.Join(", ", Components.Select(c => c.ToString()))}]";
    }

    public override int GetHashCode()
    {
        return Id;
    }
}