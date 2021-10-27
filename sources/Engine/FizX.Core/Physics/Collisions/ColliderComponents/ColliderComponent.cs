using System;
using System.ComponentModel.Design;
using FizX.Core.Actors.ActorComponents;
using FizX.Core.Geometry;
using FizX.Core.Geometry.Shapes;

namespace FizX.Core.Physics.Collisions.ColliderComponents
{
    public abstract class ColliderComponent<TShape> : ColliderComponent where TShape : Shape
    {
        public abstract override TShape Shape { get; }
    }
    
    public abstract class ColliderComponent : ActorComponent
    {
        public Guid Id { get; } = Guid.NewGuid();

        public abstract Shape Shape { get; }
        
        // public abstract void OnCollision();
        public Aabb GetBoundingBox()
        {
            var actor = GetActor();
            if (actor is null)
                throw new Exception();
                
            return Shape.GetBoundingBox(actor.Transform);
        }
    }
}