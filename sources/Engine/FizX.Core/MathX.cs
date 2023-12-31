﻿using System.Numerics;

namespace FizX.Core;

public static class MathX
{
    public static float Lerp(float firstFloat, float secondFloat, float by)
    {
        return firstFloat * (1 - by) + secondFloat * by;
    }
    
    public static Vector2 Lerp(Vector2 firstVector, Vector2 secondVector, float by)
    {
        var retX = Lerp(firstVector.X, secondVector.X, by);
        var retY = Lerp(firstVector.Y, secondVector.Y, by);
        
        return new Vector2(retX, retY);
    }
}