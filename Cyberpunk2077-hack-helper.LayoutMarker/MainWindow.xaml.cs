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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private IDialogService _dialogService;
		private IFileService _fileService;
		private ApplicationViewModel _applicationViewModel;

		public MainWindow()
		{
			_dialogService = new DefaultDialogService();
			_fileService = new JsonFileService();
			_applicationViewModel = new ApplicationViewModel(_dialogService, _fileService);

			InitializeComponent();
			DataContext = _applicationViewModel;
		}

		private void OpenCmdExecuted(object target, ExecutedRoutedEventArgs e)
		{
			string command = ((RoutedCommand)e.Command).Name;
			string targetobj = ((FrameworkElement)target).Name;
			MessageBox.Show("The " + command + " command has been invoked on target object " + targetobj);
		}
	}
}
