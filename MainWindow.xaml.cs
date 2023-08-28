using System.Windows;
using Abeslamidze_Kursovaya7.ViewModels;
using Abeslamidze_Kursovaya7.Repos;

using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Abeslamidze_Kursovaya7.Interfaces;
using System.Windows.Controls;
using Abeslamidze_Kursovaya7.Models;

namespace Abeslamidze_Kursovaya7
{
    public partial class MainWindow : Window
    {
       
        private UnitOfWork unitOfWork = new UnitOfWork();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = ViewModel = new MainWindowViewModel(unitOfWork);
        }

        public MainWindowViewModel ViewModel { get; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
            //ActivateDispatchMonitoring();
        }

        private void DataGridOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOrder = (Order)DataGrid_Orders.SelectedItem;

            if (selectedOrder != null && selectedOrder.Status == OrderStatus.Registered)
            {
                Button_Edit.IsEnabled = true;
            }

            else
            {
                Button_Edit.IsEnabled = false;
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
            ViewModel.DispatchInProgress = true;

            var result = ViewModel.Dispatch();

            ViewModel.DispatchInProgress = false;

            var message = string.Format("Сформировано грузоперевозок: {0}\nЗаявки, помещенные в очереди: {1}\nДоступно единиц транспорта: {2}",
                result.NumOfInProgressDeliveries,
                result.NumOfInQueueOrders,
                result.NumOfFreeTransport );
            MessageBox.Show(message, "Распределение заявок выполнено!");

            ViewModel.UpdateState();
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

                            Dispatcher.BeginInvoke(() =>
                            {

                                Button_Dispatch.IsEnabled = (result.NumOfFreeTransport > 0)
                              && (result.NumOfDeliverableOrders > 0);

                                ViewModel.UpdateState();
                            });
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
