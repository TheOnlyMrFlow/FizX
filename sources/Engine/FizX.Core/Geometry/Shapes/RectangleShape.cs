using System.Numerics;

namespace FizX.Core.Geometry.Shapes
{
    public class RectangleShape : Shape
    {
        public RectangleShape(float height, float width)
        {
            Height = height;
            Width = width;
        }
        
        public float Height { get; }
        public float Width { get; }
        
        public override AABB GetBoundingBox(Transform transform)
        {
            throw new System.NotImplementedException();
        }
    }
}