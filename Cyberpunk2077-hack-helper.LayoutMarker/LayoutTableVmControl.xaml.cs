using Cyberpunk2077_hack_helper.LayoutMarker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	/// <summary>
	/// Interaction logic for LayoutTableVmControl.xaml
	/// </summary>
	public partial class LayoutTableVmControl : UserControl
	{
		public static readonly DependencyProperty LayoutTableProperty = DependencyProperty.Register(
			"LayoutTable",
			typeof(LayoutTableViewModel),
			typeof(LayoutTableVmControl),
			new FrameworkPropertyMetadata(
				null,
				FrameworkPropertyMetadataOptions.AffectsMeasure |
				FrameworkPropertyMetadataOptions.AffectsRender,
				null));

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
