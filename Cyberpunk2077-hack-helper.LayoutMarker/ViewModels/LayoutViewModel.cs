using Cyberpunk2077_hack_helper.LayoutMarker.Tools;
using Cyberpunk2077_hack_helper.LayoutMarker.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class LayoutViewModel : INotifyPropertyChanged
	{
		private readonly IToolManager _toolManager;
		private LayoutTableViewModel _matrixViewModel;
		private LayoutTableViewModel _sequencesViewModel;

		private int _selectedTableIndex = 1;

		public event PropertyChangedEventHandler PropertyChanged;

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

		public LayoutViewModel(IToolManager toolManager)
		{
			_toolManager = toolManager;
			_matrixViewModel = new LayoutTableViewModel(_toolManager);
			_sequencesViewModel = new LayoutTableViewModel(_toolManager);
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}
