using System;
using System.Drawing;
using System.Windows.Input;

namespace Cyberpunk2077HackHelper.LayoutMarker.Tools
{
	public class SizeTool : ITool
	{
		public Action<Point> PositionSetter { get; private set; } = null;

		public Action<Size> SizeSetter { get; private set; } = null;

		private Point _downPostition = Point.Empty;
		private bool _isDown = false;

		public SizeTool(Action<Point> positionSetter, Action<Size> sizeSetter)
		{
			PositionSetter = positionSetter;
			SizeSetter = sizeSetter;
		}

		public void Reset()
		{
			_isDown = false;
			_downPostition = Point.Empty;
		}

		public void MouseDown(Point position, MouseButton button)
		{
			_isDown = true;
			_downPostition = position;
			PositionSetter?.Invoke(position);
		}

		public void MouseUp(Point position, MouseButton button)
		{
			_isDown = false;
			SizeSetter?.Invoke(new Size(position.X - _downPostition.X, position.Y - _downPostition.Y));
		}

		public void MouseEnter(Point position) { }

		public void MouseMove(Point position)
		{
			if (_isDown)
				SizeSetter?.Invoke(new Size(position.X - _downPostition.X, position.Y - _downPostition.Y));
		}

		public void MouseLeave(Point position) { }

		public void MouseWheel(Point position, int delta) { }
	}
}
