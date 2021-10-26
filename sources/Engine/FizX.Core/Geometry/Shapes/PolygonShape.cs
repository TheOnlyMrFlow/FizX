using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FizX.Core.Geometry.Shapes
{
    public class PolygonShape : Shape
    {
        public Vector2[] Vertices { get; }

        public PolygonShape(IReadOnlyCollection<Vector2> vertices)
        {
            var centroid = vertices.GetCentroid();
            
            Vertices = vertices
                .Select(v => new Vector2(v.X - centroid.X, v.Y - centroid.Y))
                .ToArray();
        }

        public override AABB GetBoundingBox(Transform transform)
        {
            throw new System.NotImplementedException();
        }
    }
}