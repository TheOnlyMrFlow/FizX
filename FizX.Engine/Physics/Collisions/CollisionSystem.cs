using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizX.Engine.Physics.Collisions
{
    internal static class CollisionSystem
    {
        internal static ColliderRegistry ColliderRegistry { get; private set; } = new ColliderRegistry();
        internal static CollisionRegistry CollisionRegistry { get; private set; } = new CollisionRegistry();

        internal static void RegisterCollider(Collider collider)
        {
            ColliderRegistry.RegisterCollider(collider);
        }

        internal static void UnRegisterCollider(Collider collider)
        {
            ColliderRegistry.UnRegisterCollider(collider);
        }

        public static void Run(World world)
        {

            foreach (var layer in ColliderRegistry.Layers.Keys)
            {
                var colliders = ColliderRegistry.Layers[layer].ToArray();
                UpdateCollisionsPairWise(colliders);
            }
        }

        private static void UpdateCollisionsPairWise(Collider[] colliders)
        {
            for (var i = 0; i < colliders.Length; i++)
                for (var j = i + 1; j < colliders.Length; j++)
                    UpdateCollisions(colliders[i], colliders[j]);
        }

        private static void UpdateCollisions(Collider a, Collider b)
        {
            if (Overlap(a, b))
            {
                if (CollisionRegistry.AreColliding(a, b))
                    NotifyCollisionStay(a, b);
                else
                {
                    NotifyCollisionEnter(a, b);
                    CollisionRegistry.RegisterCollision(a, b);
                }
            }
            else if (CollisionRegistry.AreColliding(a, b))
            {
                NotifyCollisionExit(a, b);
                CollisionRegistry.UnRegisterCollision(a, b);
            }
        }

        private static IEnumerable<Collision> GetCollisions(Collider[] colliders)
        {
            for (var i = 0; i < colliders.Length; i++)
                for (var j = i + 1; j < colliders.Length; j++)
                    if (TryGetCollisionBetween(colliders[i], colliders[j], out var collision))
                        yield return collision;
        }

        private static bool TryGetCollisionBetween(Collider a, Collider b, out Collision collision)
        {
            if (Overlap(a, b))
            {
                collision = new Collision(a, b);
                return true;
            }

            collision = null;
            return false;
        }

        public static bool Overlap(Collider a, Collider b)
        {
            if (a is BoxCollider aBox && b is BoxCollider bBox)
                return Overlap(aBox, bBox);

            throw new NotImplementedException();
        }

        public static bool Overlap(BoxCollider boxA, BoxCollider boxB)
        {

            var posA = boxA.GameObject.Transform.Position;
            var posB = boxB.GameObject.Transform.Position;

            return
                posA.X < posB.X + boxB.Width &&
                posA.X + boxA.Width > posB.X &&
                posA.Y < posB.Y + boxB.Height &&
                posA.Y + boxA.Height > posB.Y;
        }

        private static void NotifyCollisionEnter(Collider a, Collider b)
        {
            var collision = new Collision(a, b);
            a.GameObject.OnCollisionEnter(collision);
            b.GameObject.OnCollisionEnter(collision);
        }

        private static void NotifyCollisionStay(Collider a, Collider b)
        {
            var collision = new Collision(a, b);
            a.GameObject.OnCollisionStay(collision);
            b.GameObject.OnCollisionStay(collision);
        }

        private static void NotifyCollisionExit(Collider a, Collider b)
        {
            var collision = new Collision(a, b);
            a.GameObject.OnCollisionExit(collision);
            b.GameObject.OnCollisionExit(collision);
        }
    }
}
