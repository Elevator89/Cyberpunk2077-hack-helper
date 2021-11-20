using System.Windows;

namespace Cyberpunk2077HackHelper.LayoutMarker
{
	public static class Util
	{
		public static System.Drawing.Point Subrtract(System.Drawing.Point a, System.Drawing.Point b)
		{
			return new System.Drawing.Point(a.X - b.X, a.Y - b.Y);
		}

		public static System.Drawing.Point Add(System.Drawing.Point a, System.Drawing.Point b)
		{
			return new System.Drawing.Point(a.X + b.X, a.Y + b.Y);
		}

		public static System.Drawing.Point ToDrawingPoint(Point windowsPoint)
		{
			return new System.Drawing.Point((int)windowsPoint.X, (int)windowsPoint.Y);
		}

		public static System.Drawing.Size ToDrawingSize(Size windowsSize)
		{
			return new System.Drawing.Size((int)windowsSize.Width, (int)windowsSize.Height);
		}

		public static System.Drawing.Size ToDrawingSize(Vector windowsVector)
		{
			return new System.Drawing.Size((int)windowsVector.X, (int)windowsVector.Y);
		}

		public static System.Drawing.Size Multiply(System.Drawing.Size a, System.Drawing.Size b)
		{
			return new System.Drawing.Size(a.Width * b.Width, a.Height * b.Height);
		}

		public static System.Drawing.Size Divide(System.Drawing.Size a, System.Drawing.Size b)
		{
			return new System.Drawing.Size(a.Width / b.Width, a.Height / b.Height);
		}

		public static System.Drawing.Size ToSize(System.Drawing.Point point)
		{
			return new System.Drawing.Size(point.X, point.Y);
		}

		public static System.Drawing.Point ToPoint(System.Drawing.Size size)
		{
			return new System.Drawing.Point(size.Width, size.Height);
		}
	}
}
