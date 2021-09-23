using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using System;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class PointViewModel : INotifyPropertyChanged
	{
		private readonly Func<Point> _getter;
		private readonly Action<Point> _setter;

		public PointViewModel(Func<Point> getter, Action<Point> setter)
		{
			_getter = getter;
			_setter = setter;
		}

		public int X
		{
			get { return _getter().X; }
			set
			{
				Point point = _getter();

				if (point.X == value)
					return;

				point.X = value;
				_setter(point);
				OnPropertyChanged();
			}
		}

		public int Y
		{
			get { return _getter().Y; }
			set
			{
				Point point = _getter();

				if (point.Y == value)
					return;

				point.Y = value;
				_setter(point);
				OnPropertyChanged();
			}
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
