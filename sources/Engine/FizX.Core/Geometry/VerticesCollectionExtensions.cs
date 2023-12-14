using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FizX.Core.Geometry;

public static class VerticesCollectionExtensions
{
    public static Vector2 GetCentroid(this IReadOnlyCollection<Vector2> vertices)
    {
        var centroid = Vector2.Zero;
        var signedArea = 0f;
            
        var prev = vertices.Last();
        foreach (var v in vertices)
        {
            var partialSignedArea = prev.X * prev.Y - v.X * v.Y;
            centroid.X += (prev.X + v.X) * partialSignedArea;
            centroid.Y += (prev.Y + v.Y) * partialSignedArea;
            signedArea += partialSignedArea;
            prev = v;
        }

        centroid.X /= (3.0f * signedArea);
        centroid.Y /= (3.0f * signedArea);

        return centroid;
    }
}