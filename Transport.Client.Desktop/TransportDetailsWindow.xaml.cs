using Abeslamidze_Kursovaya7.Services;
using System.Windows;
using Transport.Client.Desktop.ViewModels;
using Transport.DTOs;

namespace Transport.Client.Desktop
{
	/// <summary>
	/// Interaction logic for TransportDetailsWindow.xaml
	/// </summary>
	public partial class TransportDetailsWindow : Window
    {
        public TransportDetailsWindow(TransportDto transport, IApiService apiService)
		{
			InitializeComponent();

			DataContext = new TransportWindowViewModel(transport, apiService);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (DataContext is TransportWindowViewModel vm)
			{
				vm.LoadData();
			}
		}
	}
}
