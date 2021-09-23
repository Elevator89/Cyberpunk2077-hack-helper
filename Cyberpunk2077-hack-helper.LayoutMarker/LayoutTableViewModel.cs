using Cyberpunk2077_hack_helper.Grabbing;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class LayoutTableViewModel : INotifyPropertyChanged
	{
		private LayoutTable _model;

		public LayoutTable Model
		{
			get { return _model; }
			set
			{
				_model = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(Position));
				OnPropertyChanged(nameof(CellSize));
				OnPropertyChanged(nameof(TestValue));
			}
		}

		public Point Position
		{
			get { return _model.Position; }
			set
			{
				if (_model.Position == value)
					return;

				_model.Position = value;
				OnPropertyChanged();
			}
		}

		public Size CellSize
		{
			get { return _model.CellSize; }
			set
			{
				if (_model.CellSize == value)
					return;

				_model.CellSize = value;
				OnPropertyChanged();
			}
		}

		public int TestValue
		{
			get { return _model.TestValue; }
			set
			{
				if (_model.TestValue == value)
					return;

				_model.TestValue = value;
				OnPropertyChanged();
			}
		}

		public LayoutTableViewModel(LayoutTable layoutTable)
		{
			_model = layoutTable;
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
