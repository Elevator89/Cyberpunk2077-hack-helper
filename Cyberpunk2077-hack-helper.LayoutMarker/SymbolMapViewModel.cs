﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using Cyberpunk2077_hack_helper.Common;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class SymbolMapViewModel : INotifyPropertyChanged
	{
		private LayoutTableViewModel _layoutTableViewModel;

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
					  Points.Add(new PointViewModel(this, new Point(0, 0)));
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

		public SymbolMapViewModel(LayoutTableViewModel layoutTableViewModel)
		{
			_layoutTableViewModel = layoutTableViewModel;
			_symbol = Symbol._1C;
			Points = new TrulyObservableCollection<PointViewModel>();
		}

		public void NotifyPointEdit(PointViewModel point)
		{
			_layoutTableViewModel.NotifyPointEdit(point);
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
