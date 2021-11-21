using Cyberpunk2077HackHelper.LayoutMarker.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Cyberpunk2077HackHelper.LayoutMarker
{
	/// <summary>
	/// Interaction logic for LayoutTableVmControl.xaml
	/// </summary>
	public partial class LayoutTableVmControl : UserControl
	{
		public static readonly DependencyProperty LayoutTableProperty = DependencyProperty.Register(
			nameof(LayoutTable),
			typeof(LayoutTableViewModel),
			typeof(LayoutTableVmControl));

		public LayoutTableViewModel LayoutTable
		{
			get { return (LayoutTableViewModel)GetValue(LayoutTableProperty); }
			set { SetValue(LayoutTableProperty, value); }
		}

		public LayoutTableVmControl()
		{
			InitializeComponent();
		}
	}
}
