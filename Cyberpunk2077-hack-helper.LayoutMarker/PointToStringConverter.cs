using System.Drawing;
using System.Windows.Data;
using System;
using System.Globalization;

namespace Cyberpunk2077_hack_helper.LayoutMarker
{
	public class PointToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Point pointViewModel = (Point)value;
			return $"{pointViewModel.X}; {pointViewModel.Y}";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string pointStr = (string)value;
			string[] coords = pointStr.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			Point point = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
			return point;
		}
	}
}
