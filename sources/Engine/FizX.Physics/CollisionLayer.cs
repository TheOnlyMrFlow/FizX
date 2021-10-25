using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FizX.Core.Geometry.Shapes;
using FizX.Core.Physics.Collisions.ColliderComponents;

namespace FizX.Physics
{
    public class CollisionLayer
    {
        private readonly ICollection<ColliderComponent> _colliders = new List<ColliderComponent>();
        
        public void AddCollider(ColliderComponent colliderComponent) 
            => _colliders.Add(colliderComponent);

        // todo: make an actual sort and sweep or other
        public IEnumerable<(ColliderComponent, ColliderComponent)> FindCandidates() =>
            _colliders
                .SelectMany(c1 => 
                    _colliders
                        .Where(c2 => c2 != c1)
                        .Where(c2 => c2.GetBoundingBox().Overlaps(c1.GetBoundingBox()))
                        .Select(c2 => (c1, c: c2)));
    }
}