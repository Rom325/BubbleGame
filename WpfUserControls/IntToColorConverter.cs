namespace WpfUserControls
{
	using System;
	using System.Globalization;
	using System.Windows.Data;
	using System.Windows.Media;

	public class IntToColorConverter : IValueConverter
	{
		protected readonly Color[] ColorValues = { Colors.BlueViolet, Colors.HotPink, Colors.DeepSkyBlue, Colors.LightGreen, Colors.DarkOrange };

		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int index = (int)value;
			if (index >= ColorValues.Length)
			{
				return null;
			}

			return ColorValues[index];
		}

		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Array.IndexOf(ColorValues, value);
		}		
	}
}