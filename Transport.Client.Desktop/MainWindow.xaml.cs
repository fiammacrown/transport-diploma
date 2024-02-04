using System.Windows;
using Abeslamidze_Kursovaya7.ViewModels;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using Abeslamidze_Kursovaya7.Services;
using Transport.DTOs;
using System.Windows.Threading;
using Refit;
using Abeslamidze_Kursovaya7.Models;

namespace Abeslamidze_Kursovaya7
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var tokenStore = new AuthTokenStore();
            var apiService = RestService.For<IApiService>(
                "https://localhost:7284/",
                new RefitSettings
                {
                    AuthorizationHeaderValueGetter = (request, ct) =>
                    {
                        return Task.FromResult(tokenStore.Token ?? string.Empty);
                    }
                });
            var authService = new AuthService(tokenStore, apiService);

			DataContext = ViewModel = new MainWindowViewModel(apiService, authService);
        }

        public MainWindowViewModel ViewModel { get; }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //await ViewModel.Initialize();
        }

        private void DataGridOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOrder = (OrderDto)DataGrid_Orders.SelectedItem;

            //if (selectedOrder != null && selectedOrder.Status == "Registered") //TODO fix
            //{
            //    Button_Edit.IsEnabled = true;
            //    Button_Delete.IsEnabled = true;
            //}

            //else
            //{
            //    Button_Edit.IsEnabled = false;
            //    Button_Delete.IsEnabled = false;
            //}
        }

        private async void EditSelected_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (OrderModel)DataGrid_Orders.SelectedItem;
            if (selectedOrder != null)
            {
                RegisterWindow registerWindow = new RegisterWindow();

                registerWindow.ViewModel.AvailableLocations = ViewModel.AvailableLocations;
                registerWindow.ViewModel.MaxAvailableTransportVolume = ViewModel.MaxAvailableTransportVolume;

                registerWindow.ViewModel.Weight = selectedOrder.Weight;
                registerWindow.ViewModel.From = selectedOrder.From;
                registerWindow.ViewModel.To = selectedOrder.To;

                registerWindow.ViewModel.CurrentOrder = new NewOrderDto
                {
                    Weight = selectedOrder.Weight,
                    From = selectedOrder.From,
                    To = selectedOrder.To,
				};

                if (registerWindow.ShowDialog() == true)
                {
                    var result = registerWindow.DataResult;
                    if (result != null)
                    {
						selectedOrder.Weight = result.Weight;
						selectedOrder.From = result.From;
						selectedOrder.To = result.To;
                        await ViewModel.UpdateOrder(selectedOrder);
                        await ViewModel.UpdateState();
                    }
                }
            }
        }
        private async void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (OrderModel)DataGrid_Orders.SelectedItem;
            if (selectedOrder != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить заявку?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    await ViewModel.DeleteOrder(selectedOrder);
                    await ViewModel.UpdateState();
                }
            }
        }

        private void Diagram1_Click(object sender, RoutedEventArgs e)
        {
            var data = GenerateRandomData();

            DiagramWindow diagramWindow = new DiagramWindow(new DeliveryCountDiagramViewModel(data));

            diagramWindow.ShowDialog();
        }


        private void Diagram2_Click(object sender, RoutedEventArgs e)
        {
            var data = GenerateRandomData();

            DiagramWindow diagramWindow = new DiagramWindow(new DeliveryTotalPriceDiagramViewModel(data));

            diagramWindow.ShowDialog();
        }

        private List<DeliveryDto> GenerateRandomData()
        {
            var data = new List<DeliveryDto>();

            Random random = new Random();

            for (int i = 1; i <= 12; i++)
            {
                int num = random.Next(1, 10);
                for (int j = 0; j < num; j++)
                {
                    int day = random.Next(1, 29);

                    double minValue = 150.0;
                    double maxValue = 1500.0;

                    double price = random.NextDouble() * (maxValue - minValue) + minValue;

                    var delivery = new DeliveryDto();

                    delivery.StartDate = new DateTime(2024, i, day);
                    delivery.Price = price;

                    data.Add(delivery);

                }
            }

            return data;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();

            registerWindow.ViewModel.AvailableLocations = ViewModel.AvailableLocations;
            registerWindow.ViewModel.MaxAvailableTransportVolume = ViewModel.MaxAvailableTransportVolume;

            if (registerWindow.ShowDialog() == true)
            {
                var result = registerWindow.DataResult;
                if (result != null)
                {
                    await ViewModel.AddNewOrder(result);
                }
            }
        }


		private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var winTitle = "Распределение заявок";

            var result = await ViewModel.Dispatch();
			var message = "Распределение заявок не может быть выполнено!";

			if (result != null)
            {
			message = string.Format("Сформировано грузоперевозок: {0}!",
			  result.Count);
			}
 

            MessageBox.Show(message, winTitle);
			await ViewModel.UpdateState();

		}

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var winTitle = "Начать выполнение";

            var result = await ViewModel.Start();
			var message = "Нет сформированных грузоперевозок!";

			if (result != null)
            {
				foreach (var newDelivery in result)
				{
					ScheduleUpdateDelivery(newDelivery);
				}

				message = string.Format("Начато выполнение {0} грузоперевозок!",
                  result.Count);

               
			}

			MessageBox.Show(message, winTitle);
			await ViewModel.UpdateState();
		}

		private async void Button_Click_3(object sender, RoutedEventArgs e)
		{
			CreateUserWindow createUserWindow = new CreateUserWindow();

			if (createUserWindow.ShowDialog() == true)
			{
				var result = createUserWindow.DataResult;
				if (result != null)
				{
					await ViewModel.AddNewUser(result);
				}
			}
		}

		private void ScheduleUpdateDelivery(DeliveryDto delivery)
		{
			DispatcherTimer timer = new DispatcherTimer();

			var interval = (delivery.EndDate - DateTime.Now).Value;
			timer.Interval = interval;

			timer.Tick += async (sender, e) =>
			{
                try
                { 
                    await ViewModel.Update(delivery.Id);
					await ViewModel.UpdateState();
                }
				finally
                {
                    timer.Stop();
                    timer = null;
				}
			};

			timer.Start();
		}

		private void ContextMenu_ContextMenuOpened(object sender, RoutedEventArgs e)
		{
			var selectedOrder = (OrderModel)DataGrid_Orders.SelectedItem;
			if (selectedOrder != null)
			{
				oEdit.IsEnabled = selectedOrder.CanEdit;
				oDelete.IsEnabled = selectedOrder.CanDelete;
			}
		}
	}
}
