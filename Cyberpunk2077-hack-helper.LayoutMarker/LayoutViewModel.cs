using Cyberpunk2077_hack_helper.Grabbing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{

	public class LayoutViewModel : INotifyPropertyChanged
	{
		private Layout _model;

		private LayoutTableViewModel _matrixViewModel;
		private LayoutTableViewModel _sequencesViewModel;

		public Layout Model
		{
			get { return _model; }
			set
			{
				_model = value;
				_matrixViewModel.Model = _model.Matrix;
				_sequencesViewModel.Model = _model.Sequences;
			}
		}

		public LayoutTableViewModel Matrix
		{
			get { return _matrixViewModel; }
		}

		public LayoutTableViewModel Sequences
		{
			get { return _sequencesViewModel; }
		}

		public LayoutViewModel(Layout model)
		{
			_model = model;
			_matrixViewModel = new LayoutTableViewModel(_model.Matrix);
			_sequencesViewModel = new LayoutTableViewModel(_model.Sequences);
		}

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
