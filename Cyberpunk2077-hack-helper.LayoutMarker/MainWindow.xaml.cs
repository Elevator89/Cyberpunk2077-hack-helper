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

		private LayoutTableVisualHost _matrixVisualHost;
		private LayoutTableVisualHost _sequencesVisualHost;

		public MainWindow()
		{
			_dialogService = new DefaultDialogService();
			_fileService = new JsonFileService();
			_applicationViewModel = new ApplicationViewModel(_dialogService, _fileService);

			InitializeComponent();
			DataContext = _applicationViewModel;
			Cursor.Fill = _probeBrush;

			_applicationViewModel.Layout.Matrix.PropertyChanged += (object sender, PropertyChangedEventArgs e) => DrawLayoutTable(((LayoutTableViewModel)sender).Model, Brushes.Red);
			_applicationViewModel.Layout.Sequences.PropertyChanged += (object sender, PropertyChangedEventArgs e) => DrawLayoutTable(((LayoutTableViewModel)sender).Model, Brushes.Blue);
		}

		private void WindowLoaded(object sender, EventArgs e)
		{
			_matrixVisualHost = new LayoutTableVisualHost(_applicationViewModel.Layout.Matrix, Brushes.Red);
			_sequencesVisualHost = new LayoutTableVisualHost(_applicationViewModel.Layout.Sequences, Brushes.Blue);
			Canvas.Children.Add(_matrixVisualHost);
			Canvas.Children.Add(_sequencesVisualHost);
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

		private void DrawLayoutTable(LayoutTable layoutTable, Brush brush)
		{
			for (int row = 0; row <= layoutTable.CellCount.Height; ++row)
			{
				Line line = new Line();
				line.X1 = layoutTable.Position.X;
				line.X2 = layoutTable.Position.X + layoutTable.CellCount.Width * layoutTable.CellSize.Width;
				line.Y1 = layoutTable.Position.Y + row * layoutTable.CellSize.Height;
				line.Y2 = layoutTable.Position.Y + row * layoutTable.CellSize.Height;
				line.Stroke = brush;
				Canvas.Children.Add(line);
			}

			for (int col = 0; col <= layoutTable.CellCount.Width; ++col)
			{
				Line line = new Line();
				line.X1 = layoutTable.Position.X + col * layoutTable.CellSize.Width;
				line.X2 = layoutTable.Position.X + col * layoutTable.CellSize.Width;
				line.Y1 = layoutTable.Position.Y;
				line.Y2 = layoutTable.Position.Y + layoutTable.CellCount.Height * layoutTable.CellSize.Height;
				line.Stroke = brush;
				Canvas.Children.Add(line);
			}
		}
	}
}
