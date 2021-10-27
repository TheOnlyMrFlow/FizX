namespace FizX.Core.Geometry.Shapes
{
    public class CircleShape : Shape
    {
        public CircleShape(float radius)
        {
            Radius = radius;
        }
        
        public float Radius { get; set; }
        
        public override Aabb GetBoundingBox(Transform transform)
        {
            var position = transform.Position;
            
            return new Aabb(position.X - Radius, position.X + Radius, position.Y - Radius, position.Y + Radius);
        }
    }
}