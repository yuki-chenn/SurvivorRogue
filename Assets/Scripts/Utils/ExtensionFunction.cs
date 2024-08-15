using UnityEngine;

public static class ExtensionFunction
{
    public static bool IsLeftOf(this Vector2 pointA, Vector2 pointB)
    {
        float crossProduct = pointA.x * pointB.y - pointB.x * pointA.y;

        // 如果叉积为正，说明 posA 在 posB 的右边
        // 如果叉积为负，说明 posA 在 posB 的左边
        return crossProduct < 0;
    }
}

