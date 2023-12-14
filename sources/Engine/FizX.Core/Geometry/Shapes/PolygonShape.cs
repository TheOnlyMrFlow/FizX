using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FizX.Core.Geometry.Shapes;

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

    public override Aabb GetBoundingBox(Transform transform)
    {
        var minX = 0f;
        var maxX = 0f;
        var minY = 0f;
        var maxY = 0f;
        
        foreach (var vertex in Vertices)
        {
            minX = Math.Min(minX, vertex.X);
            maxX = Math.Max(maxX, vertex.X);
            minY = Math.Min(minY, vertex.Y);
            maxY = Math.Max(maxY, vertex.Y);
        }
        
        minX += transform.Position.X;
        maxX += transform.Position.X;
        minY += transform.Position.Y;
        maxY += transform.Position.Y;

        return new Aabb(minX, maxX, minY, maxY);
    }
}