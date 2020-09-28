using System;
using System.Collections.Generic;
using System.Text;

namespace FizX.Engine.Physics.Collisions
{
    public class CollisionRegistry
    {
        private Dictionary<Collider, HashSet<Collider>> _colliderToCollidee = new Dictionary<Collider, HashSet<Collider>>();

        internal void RegisterCollision(Collider a, Collider b)
        {
            if (!_colliderToCollidee.TryGetValue(a, out var collideesOfA))
            {
                collideesOfA = new HashSet<Collider>();
                _colliderToCollidee.Add(a, collideesOfA);
            }
            collideesOfA.Add(b);

            if (!_colliderToCollidee.TryGetValue(b, out var collideesOfB))
            {
                collideesOfB = new HashSet<Collider>();
                _colliderToCollidee.Add(b, collideesOfB);
            }
            collideesOfB.Add(a);
        }

        internal void UnRegisterCollision(Collider a, Collider b)
        {
            _colliderToCollidee[a].Remove(b);
            _colliderToCollidee[b].Remove(a);
        }

        internal bool AreColliding(Collider a, Collider b)
        {
            if (_colliderToCollidee.TryGetValue(a, out var collidees))
            {
                return collidees.Contains(b);
            }

            return false;
        }
    }
}
