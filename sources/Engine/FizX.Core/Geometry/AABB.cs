using System;
using System.Numerics;

namespace FizX.Physics
{
    public class AABB
    {
        private float MinX { get; }
        private float MaxX { get; }
        private float MinY { get; }
        private float MaxY { get; }
        
        public AABB(float minX, float maxX, float minY, float maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }
        
        public AABB(Vector2 pointA, Vector2 pointB)
        {
            MinX = Math.Min(pointA.X, pointB.X);
            MaxX = Math.Max(pointA.X, pointB.X);
            MinY = Math.Min(pointA.Y, pointB.Y);
            MaxY = Math.Max(pointA.Y, pointB.Y);
        }

        public bool Overlaps(AABB other) => 
            (MinX <= other.MaxX && MaxX >= other.MinX) && (MinY <= other.MinY && MaxY >= other.MinY);
    }
}