using FizX.Core.Geometry.Shapes;

namespace FizX.Core.Physics.Collisions.ColliderComponents
{
    public class BoxColliderComponent : ColliderComponent<RectangleShape>
    {
        public BoxColliderComponent(float width, float height)
        {
            Shape = new RectangleShape(width, height);
        }
        
        public BoxColliderComponent(RectangleShape shape)
        {
            Shape = shape;
        }

        public override RectangleShape Shape { get; }
    }
}