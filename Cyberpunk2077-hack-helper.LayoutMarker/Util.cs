using System.Windows;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public static class Util
	{
		public static System.Drawing.Point ToDrawingPoint(Point windowsPoint)
		{
			return new System.Drawing.Point((int)windowsPoint.X, (int)windowsPoint.Y);
		}
	}
}
