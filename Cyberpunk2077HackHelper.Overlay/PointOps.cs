using GameOverlay.Drawing;
using System;

namespace Cyberpunk2077HackHelper.Overlay
{
	public static class PointOps
	{
		public static Point Add(Point a, Point b)
		{
			return new Point(a.X + b.X, a.Y + b.Y);
		}

		public static Point Subtract(Point a, Point b)
		{
			return new Point(a.X - b.X, a.Y - b.Y);
		}

		public static Point Multiply(this Point b, float a)
		{
			return Multiply(a, b);
		}

		public static Point Round(this Point p)
		{
			return new Point((int)p.X, (int)p.Y);
		}

		public static Point Multiply(float a, Point b)
		{
			return new Point(a * b.X, a * b.Y);
		}

		public static Point Divide(this Point b, float a)
		{
			return new Point(b.X / a, b.Y / a);
		}

		public static Point Normalized(this Point p)
		{
			return p.Divide(p.Magnitude());
		}

		public static float Magnitude(this Point p)
		{
			return (float)Math.Sqrt(SqrMagnitude(p));
		}

		public static float SqrMagnitude(this Point p)
		{
			return p.X * p.X + p.Y * p.Y;
		}

		public static Point RotatedLeft(this Point p)
		{
			return new Point(-p.Y, p.X);
		}

		public static Point RotatedRight(this Point p)
		{
			return new Point(p.Y, -p.X);
		}
	}
}
