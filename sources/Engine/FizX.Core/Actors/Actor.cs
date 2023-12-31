using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using FizX.Core.Actors.ActorComponents;
using FizX.Core.Geometry;
using FizX.Core.Serialization;
using FizX.Core.Timing;

namespace FizX.Core.Actors;

public class Actor
{
    private static int _lastRandomId = -1;
    
    public static int NextRandomId() => Interlocked.Increment(ref _lastRandomId);
    
    public readonly int Id = NextRandomId();
 
    [FSerializable]
    public TimeLineIndex TimeLineIndex { get; internal set; } = TimeLineIndex.TimeLine0;
    
    public TimeLine TimeLine => Time.GetTimeLine(TimeLineIndex);

    [FSerializable]
    public Transform Transform { get; private set; } = new();

    public Vector2 Position => Transform.Position;

    public float Rotation => Transform.Rotation;

    [FSerializable]
    private readonly Dictionary<Type, List<ActorComponent>> _components = new();

    public IEnumerable<ActorComponent> Components => _components.Values.SelectMany(x => x);

    public void SetTransform(Transform transform) => Transform = transform;
    
    public void SetPosition(Vector2 position) => Transform = new Transform(position, Transform.Rotation);
    
    public void SetRotation(float rotation) => Transform = new Transform(Transform.Position, rotation);
    
    public void AttachComponent(ActorComponent component)
    {
        component.Actor = this;
        var componentType = component.GetType();
        if (!_components.TryGetValue(component.GetType(), out var componentsOfType))
        {
            componentsOfType = new List<ActorComponent>();
            _components.Add(componentType, componentsOfType);
        }
        
        componentsOfType.Add(component);
    }
    
    public IReadOnlyCollection<ActorComponent> GetComponents(Type componentType)
    {
        return _components.TryGetValue(componentType, out var components) ? components : [];
    }
    
    public IReadOnlyCollection<TComponent> GetComponents<TComponent>() where TComponent : ActorComponent
    {
        return GetComponents(typeof(TComponent)).Cast<TComponent>().ToList();
    }

    public void RemoveComponent(ActorComponent component)
    {
        if (!_components.TryGetValue(component.GetType(), out var componentsOfType))
            return;

        componentsOfType.Remove(component);
        component.Actor = null;
    }

    internal void Tick(FrameInfo frame)
    {
        foreach (var component in Components) 
            component.Tick(frame);
    }
    
    public void RewindTick()
    {
        foreach (var component in Components) 
            component.RewindTick();
    }

    public void OnStartRewinding()
    {
        foreach (var component in Components) 
            component.OnStartRewinding();
    }

    public void OnStopRewinding()
    {
        foreach (var component in Components) 
            component.OnStopRewinding();
    }
    
    protected void MoveToTimeline(TimeLineIndex timeLineIndex)
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