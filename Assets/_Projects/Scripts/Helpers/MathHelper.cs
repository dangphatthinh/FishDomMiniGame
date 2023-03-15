using UnityEngine;

namespace Helpers
{
    public static class MathHelper
    {
        public static float AngleTo(Vector2 vec1, Vector2 vec2)
        {
            Vector2 vec1Rotated90 = new Vector2(-vec1.y, vec1.x);
            float sign = (Vector2.Dot(vec1Rotated90, vec2) < 0) ? -1.0f : 1.0f;
            return Vector2.Angle(vec1, vec2) * sign;
        }

        public static Vector3 GetPositionWithBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            Vector3 result = Vector3.zero;

            var tt = t * t;
            var u = (1.0f - t);
            var uu = u * u;

            result = uu * p0 + 2 * u * t * p1 + tt * p2;

            return result;
        }

        public static Vector3 DerivativeBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            Vector3 result = Vector3.zero;

            t = Mathf.Clamp01(t);
            result = 2 * ((p2 - 2 * p1 + p0) * t + p1 - p0);
            return result;
        }

        public static Vector3 GetBezierControlPoint(Vector3 start, Vector3 end, float angle, float percent = 0.3f, bool up = true)
        {
            var offset = end - start;

            var r = Mathf.Sqrt(Mathf.Pow(offset.x, 2) + Mathf.Pow(offset.y, 2));
            var theta = Mathf.Atan2(offset.y, offset.x);
            var thetaOffset = angle * Mathf.Deg2Rad;

            var r2 = (r * percent) / Mathf.Cos(thetaOffset);
            var theta2 = up ? theta + thetaOffset : theta - thetaOffset;

            var mid = new Vector3(
                r2 * Mathf.Cos(theta2) + start.y,
                r2 * Mathf.Sin(theta2) + start.x,
                0.0f
            );

            return mid;
        }

        public static int RoundToInt(float value)
        {
            return Mathf.RoundToInt(value);
        }
    }

    public class BezierInfo
    {
        public Vector3 Start = Vector3.zero;
        public Vector3 End = Vector3.zero;
        public Vector3 Control = Vector3.zero;
    }
}
