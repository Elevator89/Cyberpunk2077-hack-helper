using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using System;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class SizeViewModel : INotifyPropertyChanged
	{
		private readonly Func<Size> _getter;
		private readonly Action<Size> _setter;

		public SizeViewModel(Func<Size> getter, Action<Size> setter)
		{
			_getter = getter;
			_setter = setter;
		}

		public int Width
		{
			get { return _getter().Width; }
			set
			{
				Size point = _getter();

				if (point.Width == value)
					return;

				point.Width = value;
				_setter(point);
				OnPropertyChanged();
			}
		}

		public int Height
		{
			get { return _getter().Height; }
			set
			{
				Size point = _getter();

				if (point.Height == value)
					return;

				point.Height = value;
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
