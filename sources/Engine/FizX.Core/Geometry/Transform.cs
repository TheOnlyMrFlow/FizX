using System.Numerics;

namespace FizX.Core.Geometry
{
    public class Transform
    {
        public Transform()
        {
            
        }
        
        public Transform(Vector2 position)
        {
            Position = position;
        }
        
        public Transform(Vector2 position, float rotation) : this(position)
        {
            Rotation = rotation;
        }
        
        public Vector2 Position { get; set; } = Vector2.Zero;

        public float Rotation { get; } = 0f;
    }
}