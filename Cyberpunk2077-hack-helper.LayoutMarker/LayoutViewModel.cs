﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class LayoutViewModel : INotifyPropertyChanged
	{
		private LayoutTableViewModel _matrixViewModel;
		private LayoutTableViewModel _sequencesViewModel;

		private int _selectedTableIndex = -1;

		public LayoutTableViewModel Matrix
		{
			get { return _matrixViewModel; }
		}

		public LayoutTableViewModel Sequences
		{
			get { return _sequencesViewModel; }
		}

		public int SelectedTableIndex
		{
			get { return _selectedTableIndex; }
			set
			{
				if (_selectedTableIndex == value)
					return;
				_selectedTableIndex = value;
				OnPropertyChanged();
			}
		}

		public LayoutViewModel()
		{
			_matrixViewModel = new LayoutTableViewModel();
			_sequencesViewModel = new LayoutTableViewModel();
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
