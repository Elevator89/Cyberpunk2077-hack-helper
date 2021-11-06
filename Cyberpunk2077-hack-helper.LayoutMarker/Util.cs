using System.Windows;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public static class Util
	{
		public static System.Drawing.Point Subrtract(System.Drawing.Point a, System.Drawing.Point b)
		{
			return new System.Drawing.Point(a.X - b.X, a.Y - b.Y);
		}

		public static System.Drawing.Point ToDrawingPoint(Point windowsPoint)
		{
			return new System.Drawing.Point((int)windowsPoint.X, (int)windowsPoint.Y);
		}
	}
}
