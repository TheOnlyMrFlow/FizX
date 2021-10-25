using FizX.Core.Geometry.Shapes;

namespace FizX.Core.Physics.Collisions.ColliderComponents
{
    public class BoxColliderComponent : ColliderComponent<RectangleShape>
    {
        public BoxColliderComponent(float height, float width)
        {
            Shape = new RectangleShape(height, width);
        }
        
        public BoxColliderComponent(RectangleShape shape)
        {
            Shape = shape;
        }

        public override RectangleShape Shape { get; }
    }
}