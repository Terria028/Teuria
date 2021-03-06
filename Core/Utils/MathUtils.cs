using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Teuria;

public static class MathUtils 
{
    public static Random Randomizer = new Random();
    public const float Epsilon = 0.00000001f;



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float MoveTowards(float current, float target, float maxDelta)
    {
        if (MathF.Abs(target - current) <= maxDelta)
        {
            return target;
        }
        return current + MathF.Sign(target - current) * maxDelta;
    }

#region Vector2
    public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDelta)
    {
        var vecX = target.X - current.X;
        var vecY = target.Y - current.Y;

        var dist = vecX * vecX * vecY * vecY;

        if (dist != 0 && (maxDelta < 0 || dist > maxDelta * maxDelta))
        {
            float squaredDist = (float)Math.Sqrt(dist);

            return new Vector2(current.X + vecY / dist * maxDelta, current.Y + vecY / dist * maxDelta);
        }

        return target;
    }
    public static Vector2 DegToVec(float radians, float length) 
    {
        return new Vector2((float)Math.Cos(radians) * length, (float)Math.Sin(radians) * length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ToInt(this Vector2 vec) 
    {
        return new Vector2((int)vec.X, (int)vec.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (float, float) Destruct(this Vector2 vec) 
    {
        return (vec.X, vec.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(float value, float min, float max) 
    {
        Debug.Assert(min > max, "Minimum value is greater than Maximum");
        return value < min ? min : value > max ? max : value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Clamp(this Vector2 value, Vector2 min, Vector2 max) 
    {
        return new Vector2(Clamp(value.X, min.X, min.Y), Clamp(value.Y, min.Y, max.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Clamp(this Vector2 value, float minX, float maxX, float minY, float maxY) 
    {
        return new Vector2(Clamp(value.X, minX, maxX), Clamp(value.Y, minY, maxY));
    }
#endregion

#region Randomizer
    public static int Range(this Random random, int min, int max) => min + random.Next(max - min);
    public static float Range(this Random random, float min, float max) => min + random.NextSingle() * (max - min);
#endregion
}