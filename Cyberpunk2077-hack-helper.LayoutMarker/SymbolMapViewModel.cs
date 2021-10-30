using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using Cyberpunk2077_hack_helper.Common;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class SymbolMapViewModel : INotifyPropertyChanged
	{
		private RelayCommand _addPointCommand;
		private RelayCommand _removePointCommand;

		private Symbol _symbol;

		private int _selectedPointIndex = -1;

		public Symbol Symbol
		{
			get { return _symbol; }
			set
			{
				if (_symbol == value)
					return;

				_symbol = value;
				OnPropertyChanged();
			}
		}

		public int SelectedPointIndex
		{
			get { return _selectedPointIndex; }
			set { _selectedPointIndex = value; }
		}

		public RelayCommand AddPointCommand
		{
			get
			{
				return _addPointCommand ??
				  (_addPointCommand = new RelayCommand(obj =>
				  {
					  Points.Add(new PointViewModel(new Point(0, 0)));
				  }));
			}
		}

		public RelayCommand RemovePointCommand
		{
			get
			{
				return _removePointCommand ??
				  (_removePointCommand = new RelayCommand(obj =>
				  {
					  if (SelectedPointIndex < 0)
						  return;

					  Points.RemoveAt(SelectedPointIndex);
				  }));
			}
		}

		public TrulyObservableCollection<PointViewModel> Points { get; }

		public SymbolMapViewModel()
		{
			_symbol = Symbol._1C;
			Points = new TrulyObservableCollection<PointViewModel>();
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
