using System.Numerics;

namespace FizX.Core.Geometry.Shapes;

public abstract class Shape
{
    public abstract Aabb GetBoundingBox(Transform transform);
}