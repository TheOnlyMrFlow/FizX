using System;
using System.Collections.Generic;
using FizX.Core.Physics;
using FizX.Core.Physics.Collisions.ColliderComponents;

namespace FizX.Physics
{
    public class PhysicsEngine : IPhysicsEngine
    {
        private readonly Dictionary<string, CollisionLayer> _collisionLayers = new ();

        public void Tick(decimal deltaMs)
        {
            throw new NotImplementedException();
        }

        public void RegisterCollider(ColliderComponent colliderComponent, string layer)
        {
            if (! _collisionLayers.ContainsKey(layer))
                _collisionLayers.Add(layer, new CollisionLayer());
            
            _collisionLayers[layer].AddCollider(colliderComponent);
        }
    }
}
