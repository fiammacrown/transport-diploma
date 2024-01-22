using System.Windows;
using Abeslamidze_Kursovaya7.ViewModels;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using Abeslamidze_Kursovaya7.Services;
using Transport.DTOs;
using System.Windows.Threading;

namespace Abeslamidze_Kursovaya7
{
    public partial class MainWindow : Window
    {
       
        private readonly ApiService _apiService = new ApiService();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = ViewModel = new MainWindowViewModel(_apiService);

            // set default sorting by status for data grids
            DataGrid_Deliveries.Items.SortDescriptions.Add(new SortDescription("Status", ListSortDirection.Ascending));
            DataGrid_Orders.Items.SortDescriptions.Add(new SortDescription("Status", ListSortDirection.Ascending));
        }

        public MainWindowViewModel ViewModel { get; }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.Initialize();
            //ActivateDispatchMonitoring();
        }

        private void DataGridOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOrder = (OrderDto)DataGrid_Orders.SelectedItem;

            if (selectedOrder != null && selectedOrder.Status == "Registered") //TODO fix
            {
                Button_Edit.IsEnabled = true;
                Button_Delete.IsEnabled = true;
            }

            else
            {
                Button_Edit.IsEnabled = false;
                Button_Delete.IsEnabled = false;
            }
        }

        private async void EditSelected_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (OrderDto)DataGrid_Orders.SelectedItem;
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
            var selectedOrder = (OrderDto)DataGrid_Orders.SelectedItem;
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

                    delivery.StartDate = new DateTime(2023, i, day);
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

            ViewModel.DispatchInProgress = true;

            var result = await ViewModel.Dispatch();

            ViewModel.DispatchInProgress = false;

            if (result != null)
            {
                var message = string.Format("Сформировано грузоперевозок: {0}\nЗаявки, помещенные в очереди: {1}",
                   result.NumOfNewDeliveries,
                   result.NumOfInQueueOrders,
                   result.NumOfAssignedTransport);

                MessageBox.Show(message, winTitle);

                await ViewModel.UpdateState();

            }
            else
            {
                MessageBox.Show("Распределение заявок не может быть выполнено!\nНет заявок или доступного транспорта!", winTitle);
            }

        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var winTitle = "Начать выполнение";
            var result = await ViewModel.Start();


			if (result != null)
            {
				foreach (var newDelivery in result)
				{
					ScheduleUpdateDelivery(newDelivery);
				}

				var message = string.Format("Начато выполнение {0} грузоперевозок!",
                   result.Count);

                MessageBox.Show(message, winTitle);
				await ViewModel.UpdateState();
			}
            else
            {
                MessageBox.Show("Нет сформированных грузоперевозок!", winTitle);
            }
        }

		private void ScheduleUpdateDelivery(DeliveryDto delivery)
		{
			DispatcherTimer timer = new DispatcherTimer();

			var interval = (delivery.EndDate - DateTime.Now).Value;
			timer.Interval = interval;

			timer.Tick += async (sender, e) =>
			{
                // Function to be executed
                try
                {
					Console.WriteLine("Function called at: " + DateTime.Now);
					
                    await ViewModel.Update(delivery.Id);
					await ViewModel.UpdateState();

                    //TODO add info message 
					//Dispatcher.BeginInvoke(() =>
     //               {
     //                   var message = string.Format("Завершена грузоперевозок!", result.NumOfDoneDeliveries);

     //                   MessageBox.Show(message, "Инфо");
     //               });
                }
				finally
                {
                    // Stop the timer after the function is called
                    timer.Stop();
                    timer = null;
				}
			};

			// Start the timer
			timer.Start();
		}
	}
}
