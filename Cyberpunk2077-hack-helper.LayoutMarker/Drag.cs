using System.Windows;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class Drag
	{
		public Point Start { get; }
		public System.Drawing.Point TargetStart { get; }
		public Vector Vector { get { return End - Start; } }
		public Point End { get; private set; }
		public System.Drawing.Point TargetEnd { get; private set; }

		public Drag(Point start, System.Drawing.Point targetStart)
		{
			Start = start;
			TargetStart = targetStart;
		}

		public void Update(Point position)
		{
			End = position;
			TargetEnd = TargetStart + Util.ToDrawingSize(Vector);
		}
	}
}
