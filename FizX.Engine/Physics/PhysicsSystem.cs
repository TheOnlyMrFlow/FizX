using System;
using System.Collections.Generic;
using System.Linq;
using FizX.Engine.Physics.Collisions;
using SFML.System;

namespace FizX.Engine.Physics
{
    public static class PhysicsSystem
    {
        public static void Run(World world, float elapsed)
        {
            ApplyVelocity(world, elapsed);
            CollisionSystem.Run(world);
        }

        private static void ApplyVelocity(World world, float elapsed)
        {
            foreach (var go in world.GameObjects)
            {
                var currentPos = go.Transform.Position;
                var nextX = currentPos.X + (elapsed * go.Velocity.X);
                var nextY = currentPos.Y + (elapsed * go.Velocity.Y);
                go.Transform.Position = new Vector2f(nextX, nextY);
            }
        }
    }
}
