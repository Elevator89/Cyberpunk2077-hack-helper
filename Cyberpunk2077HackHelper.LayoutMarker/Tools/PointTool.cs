using System;
using System.Drawing;
using System.Windows.Input;

namespace Cyberpunk2077HackHelper.LayoutMarker.Tools
{
	public class PointTool : ITool
	{
		public Action<Point> Setter { get; private set; } = null;

		private bool _isDown = false;

		public PointTool(Action<Point> setter)
		{
			Setter = setter;
		}

		public void Reset()
		{
			_isDown = false;
		}

		public void MouseDown(Point position, MouseButton button)
		{
			_isDown = true;
		}

		public void MouseUp(Point position, MouseButton button)
		{
			Setter?.Invoke(position);
			_isDown = false;
		}

		public void MouseEnter(Point position) { }

		public void MouseMove(Point position)
		{
			if (_isDown)
				Setter?.Invoke(position);
		}

		public void MouseLeave(Point position) { }

		public void MouseWheel(Point position, int delta) { }
	}
}
