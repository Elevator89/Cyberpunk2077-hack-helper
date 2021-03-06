using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Collections.ObjectModel;
using Cyberpunk2077HackHelper.Common;

namespace Cyberpunk2077HackHelper.LayoutMarker.ViewModels
{
	public class SymbolMapViewModel : INotifyPropertyChanged
	{
		private RelayCommand _addPointCommand;
		private RelayCommand _removePointCommand;

		private Symbol _symbol;

		private int _selectedPointIndex = -1;

		public event PropertyChangedEventHandler PropertyChanged;

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
			set
			{
				if (_selectedPointIndex == value)
					return;

				_selectedPointIndex = value;
				OnPropertyChanged();
			}
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

		public ObservableCollection<PointViewModel> Points { get; }

		public SymbolMapViewModel()
		{
			_symbol = Symbol.Unknown;
			Points = new ObservableCollection<PointViewModel>();
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
