using System.Numerics;

namespace FizX.Core.Geometry.Shapes
{
    public abstract class Shape
    {
        public abstract AABB GetBoundingBox(Transform transform);
    }
}