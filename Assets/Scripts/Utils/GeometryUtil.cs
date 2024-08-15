using System;
using UnityEngine;

namespace Survivor.Utils
{
    class GeometryUtil
    {
        private const float PI = MathF.PI;


        public static float Angle2Rad(float angle)
        {
            return 2 * PI / 360.0f * angle;
        }

        public static Vector3 GetRadPosition(float rad, float radius = 1.0f)
        {
            return new Vector3(radius * Mathf.Cos(rad), radius * Mathf.Sin(rad), 0);
        }

        public static Vector3 GetRadPosition(Vector2 center, float rad, float radius = 1.0f)
        {
            return new Vector3(center.x + radius * Mathf.Cos(rad), center.y + radius * Mathf.Sin(rad), 0);
        }

        public static Vector3 GetAngelPosition(float angel, float radius = 1.0f)
        {
            float rad = Angle2Rad(angel);
            return GetRadPosition(rad, radius);
        }

        public static Vector3 GetAngelPosition(Vector2 center, float angel, float radius = 1.0f)
        {
            float rad = Angle2Rad(angel);
            return GetRadPosition(center, rad, radius);
        }

        public static Vector3 GetDirectionScale(Vector3 scale, Vector2 direction, string axis)
        {
            // 确保方向向量不为零
            if (direction != Vector2.zero)
            {
                // 处理 X 轴
                if (axis.Contains("x") || axis.Contains("X"))
                {
                    scale.x = direction.x < 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
                }

                // 处理 Y 轴
                if (axis.Contains("y") || axis.Contains("Y"))
                {
                    scale.y = direction.y < 0 ? Mathf.Abs(scale.y) : -Mathf.Abs(scale.y);
                }

                // 处理 Z 轴
                if (axis.Contains("z") || axis.Contains("Z"))
                {
                    // 假设我们根据 direction 的 x 或 y 分量调整 z 轴
                    scale.z = (direction.x < 0 || direction.y < 0) ? Mathf.Abs(scale.z) : -Mathf.Abs(scale.z);
                }
            }

            return scale;
        }





    }
}
