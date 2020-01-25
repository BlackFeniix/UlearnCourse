using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
        public static double GetDistanceBetweenPoints(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        public static bool GetAngle(double oppositeLine, double a, double b)
        {
            var cos = (a * a + b * b - oppositeLine * oppositeLine) / (2 * a * b);
            return cos < 0;
        }
		// Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
		{
            if (ax == x && ay == y || bx == x && by == y) return 0;

            var AB = GetDistanceBetweenPoints(ax, ay, bx, by);
            var AC = GetDistanceBetweenPoints(ax, ay, x, y);

            if (AB == 0) return AC;

            var BC = GetDistanceBetweenPoints(bx, by, x, y);
            if (GetAngle(AC, BC, AB)) return BC;
            if (GetAngle(BC, AC, AB)) return AC;

            var p = (AC + BC + AB) / 2;
            return 2 * Math.Sqrt(p * (p - AB) * (p - BC) * (p - AC)) / AB;
            //double a = ay - by;
            //double b = bx - ax;
            //double c = ax * by - bx * ay;
            //double d = Math.Abs(a * x + b * y + c) / Math.Sqrt(a * a + b * b);
            //double fromPointToEdge1 = Math.Sqrt(Math.Pow(x - ax, 2) + Math.Pow(y - ay, 2));
            //double fromPointToEdge2 = Math.Sqrt(Math.Pow(x - bx, 2) + Math.Pow(y - by, 2));
            //if (x <= ax && x <= bx && y <= ay && y <= by || x > ax && x >= bx && y >= ay && y >= by || x <= ax && x <= bx && y >= ay && y >= by || x >= ax && x >= bx && y <= ay && y <= by)
            //    return Math.Min(fromPointToEdge1, fromPointToEdge2);
            //return d;
        }
	}
}