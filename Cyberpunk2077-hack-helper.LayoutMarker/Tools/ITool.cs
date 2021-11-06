using System.Drawing;
using System.Windows.Input;

namespace Cyberpunk2077_hack_helper.LayoutMarker.Tools
{
	public interface ITool
	{
		void Reset();

		void MouseUp(Point position, MouseButton button);
		void MouseDown(Point position, MouseButton button);
		void MouseEnter(Point position);
		void MouseMove(Point position);
		void MouseLeave(Point position);
		void MouseWheel(Point position, int delta);
	}
}
