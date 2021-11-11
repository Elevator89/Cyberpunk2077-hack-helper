using System.Windows;

namespace Cyberpunk2077_hack_helper.LayoutMarker.Views
{
	public class Drag<TTargetId>
	{
		public TTargetId TargetId { get; }

		public Point Start { get; }
		public System.Drawing.Point TargetStart { get; }
		public Vector Vector { get { return End - Start; } }
		public Point End { get; private set; }
		public System.Drawing.Point TargetEnd { get; private set; }

		public Drag(TTargetId id, Point start, System.Drawing.Point targetStart)
		{
			TargetId = id;
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
