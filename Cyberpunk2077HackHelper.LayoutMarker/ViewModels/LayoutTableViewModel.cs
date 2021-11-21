using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace Cyberpunk2077HackHelper.LayoutMarker.ViewModels
{
	public class LayoutTableViewModel : INotifyPropertyChanged
	{
		private Point _position = Point.Empty;
		private Size _cellSize = Size.Empty;
		private Size _cellCount = Size.Empty;

		public event PropertyChangedEventHandler PropertyChanged;

		public Point Position
		{
			get { return _position; }
			set
			{
				if (_position == value)
					return;

				_position = value;
				OnPropertyChanged();
			}
		}

		public Size CellSize
		{
			get { return _cellSize; }
			set
			{
				if (_cellSize == value)
					return;

				_cellSize = value;
				OnPropertyChanged();
			}
		}

		public Size CellCount
		{
			get { return _cellCount; }
			set
			{
				if (_cellCount == value)
					return;

				_cellCount = value;
				OnPropertyChanged();
			}
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
