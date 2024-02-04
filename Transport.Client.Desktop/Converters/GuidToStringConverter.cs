using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Abeslamidze_Kursovaya7.Converters
{
	[ValueConversion(typeof(Guid), typeof(string))]
	public class GuidToStringConverter: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value.ToString().Split('-')[0];
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
