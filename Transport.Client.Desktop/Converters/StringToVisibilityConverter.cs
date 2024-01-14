using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Abeslamidze_Kursovaya7.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class StringToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public StringToVisibilityConverter()
        {
            // set defaults
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            bool flag = false;

            if (value is string str)
            {
                flag = !string.IsNullOrEmpty(str);
            }
            var reverse = parameter as string;
            if (reverse != null && reverse == "Reverse")
                flag = !flag;

            return flag ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
