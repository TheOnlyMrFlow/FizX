using System.Numerics;
using FizX.Physics;

namespace FizX.Core.Geometry.Shapes
{
    public abstract class Shape
    {
        public abstract AABB GetBoundingBox(Transform transform);
    }
}