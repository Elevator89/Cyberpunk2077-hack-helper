using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using Cyberpunk2077HackHelper.LayoutMarker.Tools;
using System.Collections.ObjectModel;

namespace Cyberpunk2077HackHelper.LayoutMarker.ViewModels
{
	public class LayoutTableViewModel : INotifyPropertyChanged
	{
		private readonly IToolManager _toolManager;

		private RelayCommand _addSymbolMapCommand;
		private RelayCommand _removeSymbolMapCommand;
		private RelayCommand _positionAndSizeEditCommand;

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

		public ObservableCollection<SymbolMapViewModel> SymbolMaps { get; }

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

		public ObservableCollection<PointViewModel> SelectedSymbolMapPoints
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
					  SymbolMaps.Add(new SymbolMapViewModel(this, _toolManager));
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
					  if (SelectedSymbolMap?.SelectedPointIndex >= 0)
						  SelectedSymbolMap.Points[SelectedSymbolMap.SelectedPointIndex].X++;
				  }));
			}
		}

		public RelayCommand PositionAndSizeEditCommand
		{
			get
			{
				return _positionAndSizeEditCommand ??
				  (_positionAndSizeEditCommand = new RelayCommand(obj =>
				  {
					  _toolManager.ActivateTool(new SizeTool(point => Position = point, size => CellSize = size));
				  }));
			}
		}

		public LayoutTableViewModel(IToolManager toolManager)
		{
			_toolManager = toolManager;
			SymbolMaps = new ObservableCollection<SymbolMapViewModel>();
			_selectedSymbolMapIndex = -1;
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
