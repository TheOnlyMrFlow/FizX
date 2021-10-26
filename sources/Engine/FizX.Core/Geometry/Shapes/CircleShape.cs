using System.Numerics;

namespace FizX.Core.Geometry.Shapes
{
    public class CircleShape : Shape
    {
        public float Radius { get; set; }

        public override AABB GetBoundingBox(Transform transform)
        {
            var position = transform.Position;
            
            return new AABB(position.X - Radius, position.X + Radius, position.Y - Radius, position.Y + Radius);
        }
    }
}