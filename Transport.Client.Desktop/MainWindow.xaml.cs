﻿using System.Windows;
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
using Transport.Client.Desktop;
using System.Windows.Input;
using Transport.Client.Desktop.Models;
using System.Linq;

namespace Abeslamidze_Kursovaya7
{
	public partial class MainWindow : Window
	{
		public MainWindow()
        {
            InitializeComponent();

            var tokenStore = new AuthTokenStore();
            var apiService = RestService.For<IApiService>(
				//"https://localhost:7284/",
				"https://sabeslamidze-transport-api.azurewebsites.net/",
                new RefitSettings
                {
                    AuthorizationHeaderValueGetter = (request, ct) =>
                    {
                        return Task.FromResult(tokenStore.Token ?? string.Empty);
                    }
                });
            var authService = new AuthService(tokenStore, apiService);

            ApiService = apiService;
			DataContext = ViewModel = new MainWindowViewModel(apiService, authService);
		}

		public MainWindowViewModel ViewModel { get; }
        public IApiService ApiService { get; }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //await ViewModel.Initialize();
        }

        private void DataGridOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOrder = (OrderDto)DataGrid_Orders.SelectedItem;

            if (selectedOrder != null && selectedOrder.Status == "Registered")
            { 
                menuDelete.IsEnabled = true;
                menuEdit.IsEnabled = true;
            }

            else
            {
				menuDelete.IsEnabled = false;
				menuEdit.IsEnabled = false;
			}
        }

		private void DataGridTransports_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//var selectedOrder = (OrderDto)DataGrid_Orders.SelectedItem;

		}

		private void DataGridTransports_DoubleClick(object sender, MouseButtonEventArgs e)
        {
			var selectedTransport = (TransportDto)DataGrid_Transports.SelectedItem;
            if (selectedTransport != null)
            {
				var transportWindow = new TransportDetailsWindow(selectedTransport, ApiService);
                transportWindow.ShowDialog();
			}

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
            var message = "";
			var error = "";

            if (ViewModel.Orders.Where(o => o.Status == "Registered").Count() == 0 &&
				ViewModel.Orders.Where(o => o.Status == "InQueue").Count() == 0 )
            {
                error = "Нет зарегистрированных заявок.";
            }

            if (ViewModel.Transports.Where(t => t.Status == "Free").Count() == 0)
            {
				error = "Нет свободного транспорта.";
			}

            if (error != "")
            {
				message = string.Format("Распределение заявок не может быть выполнено!\n{0}",
                    error);
                MessageBox.Show(message, winTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
			}

            var result = await ViewModel.Dispatch();
			if (result != null)
            {
			    message = string.Format("Сформировано грузоперевозок: {0}!", result.Count);
			}
 

            MessageBox.Show(message, winTitle);
			await ViewModel.UpdateState();

		}

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var winTitle = "Начать выполнение";

			var message = "";
			var error = "";


			if (ViewModel.Deliveries.Where(d => d.Status == "New").Count() == 0)
			{
				error = "Нет сформированных грузоперевозок!";
				MessageBox.Show(error, winTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
			}

			var result = await ViewModel.Start();

			if (result != null)
            {
				await ViewModel.UpdateState();

				foreach (var newDelivery in result)
				{
                    var currentTime = DateTime.Now;
					ScheduleUpdateDelivery(newDelivery, currentTime);

                    // set progress bar updates
					var observDelivery = ViewModel.Deliveries.FirstOrDefault(x => x.Id == newDelivery.Id);
					if (observDelivery != null)
					{
                        UpdateProgress(observDelivery, newDelivery, currentTime);
					}
				}

				message = string.Format("Начато выполнение {0} грузоперевозок!", result.Count);
			}

			MessageBox.Show(message, winTitle);
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

		private void ScheduleUpdateDelivery(DeliveryDto delivery, DateTime currentTime)
		{
			DispatcherTimer timer = new DispatcherTimer();

			var interval = (delivery.EndDate - currentTime).Value;
			timer.Interval = interval;

			timer.Tick += async (sender, e) =>
			{
                try
                { 
                    await ViewModel.Update(delivery.Id);
					await ViewModel.UpdateState();

					var statusList = new List<string> { "InQueue", "Registered" };
					var newOrders = ViewModel.Orders.Where(o => statusList.Contains(o.Status)).ToList();

					foreach (var order in newOrders)
                    {
                        if (delivery.Transport.Volume >= order.Weight)
                        {
                            var message = string.Format(
                                "Заявка {0} может быть выполнена транспортом {1}.",
                                order.Id,
                                delivery.Transport.Name);

							MessageBox.Show(message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
						}
                    }
                }
                catch (Exception ex)
                {
					MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				finally
                {
                    timer.Stop();
                    timer = null;
				}
			};

			timer.Start();
		}

        private void UpdateProgress(DeliveryModel observDelivery, DeliveryDto delivery, DateTime currentTime)
        {
			DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render);

            TimeSpan total = (delivery.EndDate - currentTime).Value;
            double step = 100 / total.TotalSeconds;

			timer.Interval = TimeSpan.FromSeconds(1);

			timer.Tick += (sender, e) =>
			{
			    if (observDelivery.Progress >= 100)
                {
                    timer.Stop();
					timer = null;
				}
                else
                {
                    observDelivery.Progress += step;
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
