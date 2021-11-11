using Cyberpunk2077_hack_helper.LayoutMarker.ViewModels;
using System.Drawing;
using System.Windows.Input;

namespace Cyberpunk2077_hack_helper.LayoutMarker.Tools
{
	public class PointVmTool : ITool
	{
		private readonly Point _basePosition;
		private readonly PointViewModel _pointViewModel;

		private bool _isDown = false;

		public PointVmTool(Point basePosition, PointViewModel pointViewModel)
		{
			_basePosition = basePosition;
			_pointViewModel = pointViewModel;
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
			_pointViewModel.Point = Util.Subrtract(position, _basePosition);
			_isDown = false;
		}

		public void MouseEnter(Point position) { }

		public void MouseMove(Point position)
		{
			if (_isDown)
				_pointViewModel.Point = Util.Subrtract(position, _basePosition);
		}

		public void MouseLeave(Point position) { }

		public void MouseWheel(Point position, int delta) { }
	}
}
