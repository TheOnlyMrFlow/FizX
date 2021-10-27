using System;
using System.Numerics;

namespace FizX.Core.Geometry
{
    public class Aabb
    {
        public float MinX { get; }
        public float MaxX { get; }
        public float MinY { get; }
        public float MaxY { get; }
        
        public Aabb(float minX, float maxX, float minY, float maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }
        
        public Aabb(Vector2 pointA, Vector2 pointB)
        {
            MinX = Math.Min(pointA.X, pointB.X);
            MaxX = Math.Max(pointA.X, pointB.X);
            MinY = Math.Min(pointA.Y, pointB.Y);
            MaxY = Math.Max(pointA.Y, pointB.Y);
        }

        public bool Overlaps(Aabb other) =>
            (MinX <= other.MaxX && MaxX >= other.MinX) &&
            (MinY <= other.MaxY && MaxY >= other.MinY);
    }
}