using Cyberpunk2077_hack_helper.Grabbing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		private SolidColorBrush _probeBrush = new SolidColorBrush();

		public MainWindow()
		{
			_dialogService = new DefaultDialogService();
			_fileService = new JsonFileService();
			_applicationViewModel = new ApplicationViewModel(_dialogService, _fileService);

			InitializeComponent();
			DataContext = _applicationViewModel;
			Cursor.Fill = _probeBrush;
		}

		private void WindowLoaded(object sender, EventArgs e)
		{
		}

		private void OpenCmdExecuted(object target, ExecutedRoutedEventArgs e)
		{
			string command = ((RoutedCommand)e.Command).Name;
			string targetobj = ((FrameworkElement)target).Name;
			MessageBox.Show("The " + command + " command has been invoked on target object " + targetobj);
		}

		private void CloseCmdExecuted(object target, ExecutedRoutedEventArgs e)
		{
			Close();
		}

		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			Point mousePos = e.GetPosition(Image);

			ImageSource imageSource = Image.Source;
			BitmapSource bitmap = (BitmapSource)imageSource;

			Color color = GetPixel(bitmap, mousePos);
			_probeBrush.Color = color;
		}

		private Color GetPixel(BitmapSource source, Point position)
		{
			if (source.Format != PixelFormats.Bgra32)
				source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);

			// Stride = (width) x (bytes per pixel)
			int bytesPerPixel = source.Format.BitsPerPixel / 8;
			int stride = source.PixelWidth * bytesPerPixel; // bytes in one line
			byte[] pixel = new byte[bytesPerPixel];

			int x = (int)position.X;
			int y = (int)position.Y;

			Int32Rect rect = new Int32Rect(x, y, 1, 1);
			source.CopyPixels(rect, pixel, stride, 0);

			return Color.FromRgb(pixel[2], pixel[1], pixel[0]);
		}
	}
}
