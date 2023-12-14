namespace FizX.Core.Geometry.Shapes;

public class CircleShape(float radius) : Shape
{
    public float Radius { get; } = radius;

    public override Aabb GetBoundingBox(Transform transform)
    {
        var position = transform.Position;
            
        return new Aabb(position.X - Radius, position.X + Radius, position.Y - Radius, position.Y + Radius);
    }
}