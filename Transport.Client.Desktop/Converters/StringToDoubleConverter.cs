using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Abeslamidze_Kursovaya7.Converters
{
    [ValueConversion(typeof(string), typeof(double))]
    public class StringToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string srtValue)
            {
                if (double.TryParse(srtValue, out double num))
                {
                    return num;
                }
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
