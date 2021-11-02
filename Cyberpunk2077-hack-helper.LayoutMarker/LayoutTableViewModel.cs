using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class LayoutTableViewModel : INotifyPropertyChanged
	{
		private LayoutViewModel _layoutViewModel;
		private RelayCommand _addSymbolMapCommand;
		private RelayCommand _removeSymbolMapCommand;

		private RelayCommand _debugCommand;
		private RelayCommand _cellSizeEditCommand;
		private RelayCommand _positionEditCommand;

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
				OnPropertyChanged(nameof(SelectedSymbolMap));
				OnPropertyChanged(nameof(SelectedSymbolMapPoints));
			}
		}

		public SymbolMapViewModel SelectedSymbolMap
		{
			get
			{
				if (SelectedSymbolMapIndex < 0 || SelectedSymbolMapIndex >= SymbolMaps.Count)
					return null;

				return SymbolMaps[SelectedSymbolMapIndex];
			}
		}

		public TrulyObservableCollection<PointViewModel> SelectedSymbolMapPoints
		{
			get { return SelectedSymbolMap?.Points; }
		}

		public RelayCommand AddSymbolMapCommand
		{
			get
			{
				return _addSymbolMapCommand ??
				  (_addSymbolMapCommand = new RelayCommand(obj =>
				  {
					  SymbolMaps.Add(new SymbolMapViewModel(this));
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

		public RelayCommand PositionEditCommand
		{
			get
			{
				return _positionEditCommand ??
				  (_positionEditCommand = new RelayCommand(obj =>
				  {
					  _layoutViewModel.NotifyLayoutTablePositionEdit(this);
				  }));
			}
		}

		public RelayCommand CellSizeEditCommand
		{
			get
			{
				return _cellSizeEditCommand ??
				  (_cellSizeEditCommand = new RelayCommand(obj =>
				  {
					  _layoutViewModel.NotifyLayoutTableCellSizeEdit(this);
				  }));
			}
		}

		public LayoutTableViewModel(LayoutViewModel layoutViewModel)
		{
			_layoutViewModel = layoutViewModel;
			SymbolMaps = new TrulyObservableCollection<SymbolMapViewModel>();
			_selectedSymbolMapIndex = -1;
		}

		public void NotifyPointEdit(PointViewModel point)
		{
			_layoutViewModel.NotifyPointEdit(point);
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
