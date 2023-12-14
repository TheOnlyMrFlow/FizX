using System.Numerics;

namespace FizX.Core.Geometry.Shapes;

public class RectangleShape(float width, float height) : Shape
{
    public float Height { get; } = height;
    public float Width { get; } = width;

    public override Aabb GetBoundingBox(Transform transform)
        => new Aabb(
            transform.Position.X - (Width / 2f),
            transform.Position.X + (Width / 2f),
            transform.Position.Y - (Height / 2f), 
            transform.Position.Y + (Height / 2f));
}