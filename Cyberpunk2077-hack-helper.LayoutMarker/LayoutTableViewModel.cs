using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class LayoutTableViewModel : INotifyPropertyChanged
	{
		private RelayCommand _addSymbolMapCommand;
		private RelayCommand _removeSymbolMapCommand;

		private RelayCommand _debugCommand;

		private Point _position = Point.Empty;
		private Size _cellSize = Size.Empty;
		private Size _cellCount = Size.Empty;

		private int _selectedSymbolMapIndex;

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

		public int SelectedSymbolMapIndex
		{
			get { return _selectedSymbolMapIndex; }
			set
			{
				_selectedSymbolMapIndex = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(SelectedSymbolMapPoints));
			}
		}

		public TrulyObservableCollection<PointViewModel> SelectedSymbolMapPoints
		{
			get
			{
				if (SelectedSymbolMapIndex < 0 || SelectedSymbolMapIndex >= SymbolMaps.Count)
					return null;

				return SymbolMaps[SelectedSymbolMapIndex].Points;
			}
		}

		public RelayCommand AddSymbolMapCommand
		{
			get
			{
				return _addSymbolMapCommand ??
				  (_addSymbolMapCommand = new RelayCommand(obj =>
				  {
					  SymbolMaps.Add(new SymbolMapViewModel());
				  }));
			}
		}

		public RelayCommand RemoveSymbolMapCommand
		{
			get
			{
				return _removeSymbolMapCommand ??
				  (_removeSymbolMapCommand = new RelayCommand(obj =>
				  {
					  SymbolMaps.RemoveAt(SelectedSymbolMapIndex);
				  }));
			}
		}

		public RelayCommand DebugCommand
		{
			get
			{
				return _debugCommand ??
				  (_debugCommand = new RelayCommand(obj =>
				  {
					  SymbolMapViewModel selectedSymbolMap = SymbolMaps[SelectedSymbolMapIndex];
					  if (selectedSymbolMap.SelectedPointIndex >= 0)
						  selectedSymbolMap.Points[selectedSymbolMap.SelectedPointIndex].X++;
				  }));
			}
		}

		public TrulyObservableCollection<SymbolMapViewModel> SymbolMaps { get; }

		public LayoutTableViewModel()
		{
			SymbolMaps = new TrulyObservableCollection<SymbolMapViewModel>();
			_selectedSymbolMapIndex = -1;
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
