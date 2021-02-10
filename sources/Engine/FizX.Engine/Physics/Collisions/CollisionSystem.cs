using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizX.Engine.Physics.Collisions
{
    internal enum ColliderPairState
    {
        StartColliding,
        KeepColliding,
        ExitColliding,
        KeepNotColliding
    }

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

        public static void Run(World world, float elapsed)
        {

            foreach (var layer in ColliderRegistry.Layers.Keys)
            {
                RunLayerWilde(layer, elapsed);
            }
        }

        internal static void RunLayerWilde(int layer, float elapsed)
        {
            foreach (var colliderPair in GetColliderPairs(layer))
            {
                switch (GetColliderPairState(colliderPair))
                {
                    case ColliderPairState.StartColliding:
                        NotifyCollisionEnter(colliderPair);
                        CollisionRegistry.RegisterCollision(colliderPair);
                        ResolveCollision(colliderPair, elapsed);

                        break;
                    case ColliderPairState.KeepColliding:
                        NotifyCollisionStay(colliderPair);
                        ResolveCollision(colliderPair, elapsed);

                        break;
                    case ColliderPairState.ExitColliding:
                        NotifyCollisionExit(colliderPair);
                        CollisionRegistry.UnRegisterCollision(colliderPair);

                        break;
                    case ColliderPairState.KeepNotColliding:

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        internal static IEnumerable<Tuple<Collider, Collider>> GetColliderPairs(int layer)
        {
            var colliders = ColliderRegistry.Layers[layer].ToArray();
            for (var i = 0; i < colliders.Length; i++)
            for (var j = i + 1; j < colliders.Length; j++)
                yield return new Tuple<Collider, Collider>(colliders[i], colliders[j]);
        }


        private static void ResolveCollision(Tuple<Collider, Collider> colliderPair, float elapsed)
        {
            Console.WriteLine("Resolving");
            var go1 = colliderPair.Item1.GameObject;
            var go2 = colliderPair.Item2.GameObject;
            while (Overlap(colliderPair.Item1, colliderPair.Item2))
            {
                go1.Transform.Position -= go1.Velocity * elapsed;
                go2.Transform.Position -= go2.Velocity * elapsed;
            }
        }

        private static ColliderPairState GetColliderPairState(Tuple<Collider, Collider> colliderPair)
        {
            if (Overlap(colliderPair))
                return CollisionRegistry.AreColliding(colliderPair)
                    ? ColliderPairState.KeepColliding
                    : ColliderPairState.StartColliding;

            return CollisionRegistry.AreColliding(colliderPair)
                ? ColliderPairState.ExitColliding
                : ColliderPairState.KeepNotColliding;
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

        public static bool Overlap(Tuple<Collider, Collider> colliderPair)
        {
            return Overlap(colliderPair.Item1, colliderPair.Item2);
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

        private static void NotifyCollisionEnter(Tuple<Collider, Collider> colliderPair)
        {
            var collision = new Collision(colliderPair.Item1, colliderPair.Item2);
            colliderPair.Item1.GameObject.OnCollisionEnter(collision);
            colliderPair.Item2.GameObject.OnCollisionEnter(collision);
        }

        private static void NotifyCollisionStay(Tuple<Collider, Collider> colliderPair)
        {
            var collision = new Collision(colliderPair.Item1, colliderPair.Item2);
            colliderPair.Item1.GameObject.OnCollisionStay(collision);
            colliderPair.Item2.GameObject.OnCollisionStay(collision);
        }

        private static void NotifyCollisionExit(Tuple<Collider, Collider> colliderPair)
        {
            var collision = new Collision(colliderPair.Item1, colliderPair.Item2);
            colliderPair.Item1.GameObject.OnCollisionExit(collision);
            colliderPair.Item2.GameObject.OnCollisionExit(collision);
        }
    }
}
