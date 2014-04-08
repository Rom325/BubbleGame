using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfUserControls
{
	using System.Windows;

	public class IntToBrushConverter : IntToColorConverter
    {
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object color = base.Convert(value, targetType, parameter, culture);
			if (color == null)
			{
				return null;
			}

			return new RadialGradientBrush(new GradientStopCollection
            {
                new GradientStop {Color = (Color)color, Offset = 1.0},
                new GradientStop {Color = Colors.White, Offset = 0.0}
            }){GradientOrigin = new Point(0.25, 0.25)};
		}

    }
}
