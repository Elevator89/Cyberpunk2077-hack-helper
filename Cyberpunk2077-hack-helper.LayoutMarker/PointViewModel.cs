using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class PointViewModel : INotifyPropertyChanged
	{
		private Point _point;

		public Point Point
		{
			get { return _point; }
			set
			{
				if (_point == value)
					return;

				_point = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(X));
				OnPropertyChanged(nameof(Y));
			}
		}

		public int X
		{
			get { return _point.X; }
			set
			{
				if (_point.X == value)
					return;

				_point.X = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(Point));
			}
		}

		public int Y
		{
			get { return _point.Y; }
			set
			{
				if (_point.Y == value)
					return;

				_point.Y = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(Point));
			}
		}

		public PointViewModel(Point point)
		{
			_point = point;
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
