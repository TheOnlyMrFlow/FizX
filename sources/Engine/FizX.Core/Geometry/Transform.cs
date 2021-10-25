using System.Numerics;

namespace FizX.Core.Geometry
{
    public class Transform
    {
        public Vector2 Position { get; } = Vector2.Zero;

        public float Rotation { get; } = 0f;
    }
}