using System;
using System.Numerics;

namespace FizX.Core.Geometry;

public class Aabb(float minX, float maxX, float minY, float maxY)
{
    public float MinX { get; } = minX;
    public float MaxX { get; } = maxX;
    public float MinY { get; } = minY;
    public float MaxY { get; } = maxY;

    public Aabb(Vector2 pointA, Vector2 pointB) : this(Math.Min(pointA.X, pointB.X), Math.Max(pointA.X, pointB.X), Math.Min(pointA.Y, pointB.Y), Math.Max(pointA.Y, pointB.Y))
    {
    }

    public bool Overlaps(Aabb other) =>
        (MinX <= other.MaxX && MaxX >= other.MinX) &&
        (MinY <= other.MaxY && MaxY >= other.MinY);
}