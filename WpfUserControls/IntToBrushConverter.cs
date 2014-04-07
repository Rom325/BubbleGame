using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfUserControls
{
    public class IntToBrushConverter : IValueConverter
    {
        protected readonly Color[] ColorValues = {Colors.BlueViolet, Colors.HotPink, Colors.Aqua, Colors.LightGreen};

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int) value;
            if (index >= ColorValues.Length )
            {
                return null;
            }

            Color color = ColorValues[index];


            return new LinearGradientBrush(new GradientStopCollection
            {
                new GradientStop {Color = color, Offset = 0.0},
                new GradientStop {Color = Colors.White, Offset = 1.0}
            });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Array.IndexOf(ColorValues, value);
        }
    }
}
