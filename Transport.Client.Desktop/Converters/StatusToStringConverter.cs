using System;
using System.Globalization;
using System.Windows.Data;

namespace Abeslamidze_Kursovaya7.Converters
{
	[ValueConversion(typeof(string), typeof(string))]
	class DeliveryStatusToStringConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value.ToString() == "New")
			{
				return "Создана";

			}
			else if (value.ToString() == "InProgress")
			{
				return "Выполняется";
			}

			return "Завершена";

		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	[ValueConversion(typeof(string), typeof(string))]
	class TransportStatusToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value.ToString() == "InTransit")
			{
				return "В пути";

			}
			else if (value.ToString() == "Assigned")
			{
				return "Забронирован";
			}

			return "Свободен";

		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}


	[ValueConversion(typeof(string), typeof(string))]
	class OrderStatusToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value.ToString() == "Registered")
			{
				return "Зарегистрирована";

			}
			else if (value.ToString() == "Assigned")
			{
				return "Сформирована";
			}
			else if (value.ToString() == "InProgress")
			{
				return "Выполняется";
			}
			else if (value.ToString() == "InQueue")
			{
				return "В очереди";
			}

			return "Завершена";

		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}