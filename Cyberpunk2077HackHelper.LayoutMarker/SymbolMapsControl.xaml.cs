using System.Windows;
using System.Windows.Controls;
using Cyberpunk2077HackHelper.LayoutMarker.ViewModels;

namespace Cyberpunk2077HackHelper.LayoutMarker
{
	/// <summary>
	/// Interaction logic for SymbolMapsControl.xaml
	/// </summary>
	public partial class SymbolMapsControl : UserControl
	{
		public static readonly DependencyProperty SymbolMapsProperty = DependencyProperty.Register(
			nameof(SymbolMaps),
			typeof(SymbolMapsViewModel),
			typeof(SymbolMapsControl));

		public SymbolMapsViewModel SymbolMaps
		{
			get { return (SymbolMapsViewModel)GetValue(SymbolMapsProperty); }
			set { SetValue(SymbolMapsProperty, value); }
		}

		public SymbolMapsControl()
		{
			InitializeComponent();
		}
	}
}
