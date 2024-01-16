﻿using System.Windows;
using Abeslamidze_Kursovaya7.ViewModels;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Abeslamidze_Kursovaya7.Models;
using System.ComponentModel;

namespace Abeslamidze_Kursovaya7
{
    public partial class MainWindow : Window
    {
       
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = ViewModel = new MainWindowViewModel(_unitOfWork);

            // set default sorting by status for data grids
            DataGrid_Deliveries.Items.SortDescriptions.Add(new SortDescription("Status", ListSortDirection.Ascending));
            DataGrid_Orders.Items.SortDescriptions.Add(new SortDescription("Status", ListSortDirection.Ascending));
        }

        public MainWindowViewModel ViewModel { get; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
            ActivateDispatchMonitoring();
        }

        private void DataGridOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOrder = (Order)DataGrid_Orders.SelectedItem;

            if (selectedOrder != null && selectedOrder.Status == OrderStatus.Registered)
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

        private void EditSelected_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (Order)DataGrid_Orders.SelectedItem;
            if (selectedOrder != null)
            {
                RegisterWindow registerWindow = new RegisterWindow();


                registerWindow.ViewModel.AvailableLocations = ViewModel.AvailableLocations;
                registerWindow.ViewModel.MaxAvailableTransportVolume = ViewModel.MaxAvailableTransportVolume;

                registerWindow.ViewModel.Weight = selectedOrder.Weight;
                registerWindow.ViewModel.From = selectedOrder.From;
                registerWindow.ViewModel.To = selectedOrder.To;

                registerWindow.ViewModel.CurrentOrder = selectedOrder;

                if (registerWindow.ShowDialog() == true)
                {
                    var result = registerWindow.DataResult;
                    if (result != null)
                    {
                        ViewModel.UpdateOrder(result);
                        ViewModel.UpdateState();

                    }
                }
            }
        }
        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (Order)DataGrid_Orders.SelectedItem;
            if (selectedOrder != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить заявку?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    ViewModel.DeleteOrder(selectedOrder);
                    ViewModel.UpdateState();
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

        private List<Delivery> GenerateRandomData()
        {
            var data = new List<Delivery>();

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

                    var delivery = new Delivery();

                    delivery.StartDate = new DateTime(2023, i, day);
                    delivery.Price = price;

                    data.Add(delivery);

                }
            }

            return data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();

            registerWindow.ViewModel.AvailableLocations = ViewModel.AvailableLocations;
            registerWindow.ViewModel.MaxAvailableTransportVolume = ViewModel.MaxAvailableTransportVolume;

            if (registerWindow.ShowDialog() == true)
            {
                var result = registerWindow.DataResult;
                if (result != null)
                {
                    ViewModel.AddNewOrder(result);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var winTitle = "Распределение заявок";

            ViewModel.DispatchInProgress = true;

            var result = ViewModel.Dispatch();

            ViewModel.DispatchInProgress = false;

            if (result != null)
            {
                var message = string.Format("Сформировано грузоперевозок: {0}\nЗаявки, помещенные в очереди: {1}\nЗадействовано единиц транспорта: {2}",
                   result.NumOfNewDeliveries,
                   result.NumOfInQueueOrders,
                   result.NumOfAssignedTransport);

                MessageBox.Show(message, winTitle);

                ViewModel.UpdateState();

            }
            else
            {
                MessageBox.Show("Распределение заявок не может быть выполнено!\nНет зарегистированных заявок или доступного транспорта!", winTitle);
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var winTitle = "Начать выполнение";
            var result = ViewModel.Start();

            if (result != null)
            {
                var message = string.Format("Начато выполнение {0} грузоперевозок!",
                   result.NumOfInProgressDeliveries);

                MessageBox.Show(message, winTitle);

                ViewModel.UpdateState();

            }
            else
            {
                MessageBox.Show("Нет сформированных грузоперевозок!", winTitle);
            }
       
        }

        private async void ActivateDispatchMonitoring()
        {
            try
            {
                await Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(1000);

                        if (!ViewModel.DispatchInProgress)
                        {
                            var result = ViewModel.Update();

                            if (result != null && result.NumOfDoneDeliveries > 0)
                            {
                                Dispatcher.BeginInvoke(() =>
                                {
                                    var message = string.Format("Завершено {0} грузоперевозок!", result.NumOfDoneDeliveries);

                                    MessageBox.Show(message, "Инфо");
                                    ViewModel.UpdateState();
                                });
                            }
                           
                        }
                       
                    }

                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}