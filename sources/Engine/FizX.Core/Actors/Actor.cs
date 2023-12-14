using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FizX.Core.Actors.ActorComponents;
using FizX.Core.Geometry;

namespace FizX.Core.Actors;

public class Actor
{
    public int Id => 0;

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

    internal void Tick(int deltaMs)
    {
        foreach (var component in _components) 
            component.Tick(deltaMs);
    }

    public override string ToString()
    {
        return $"[Actor:{Id}] [Transform:{Transform}] [Components: {string.Join(", ", Components.Select(c => c.ToString()))}]";
    }
}