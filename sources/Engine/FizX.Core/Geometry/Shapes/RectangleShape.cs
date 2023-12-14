using System.Numerics;

namespace FizX.Core.Geometry.Shapes
{
    public class RectangleShape : Shape
    {
        public RectangleShape(float width, float height)
        {
            Width = width;
            Height = height;
        }
        
        public float Height { get; }
        public float Width { get; }

        public override Aabb GetBoundingBox(Transform transform)
            => new Aabb(
                transform.Position.X - (Width / 2f),
                transform.Position.X + (Width / 2f),
                transform.Position.Y - (Height / 2f), 
                transform.Position.Y + (Height / 2f));
    }
}