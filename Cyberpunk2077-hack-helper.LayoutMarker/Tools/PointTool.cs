using System;
using System.Drawing;
using System.Windows.Input;

namespace Cyberpunk2077_hack_helper.LayoutMarker.Tools
{
	public class PointTool : ITool
	{
		public Action<Point> Setter { get; set; } = null;

		private bool _isDown = false;

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
