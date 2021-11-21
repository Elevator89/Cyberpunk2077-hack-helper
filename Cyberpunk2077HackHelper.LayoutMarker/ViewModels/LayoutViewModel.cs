using Cyberpunk2077HackHelper.LayoutMarker.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Cyberpunk2077HackHelper.LayoutMarker
{
	public class LayoutViewModel : INotifyPropertyChanged
	{
		private int _selectedTableIndex = 1;

		public event PropertyChangedEventHandler PropertyChanged;

		public LayoutTableViewModel Matrix { get; }
		public LayoutTableViewModel Sequences { get; }

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
			Matrix = new LayoutTableViewModel();
			Sequences = new LayoutTableViewModel();
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
