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
using System.Data.Entity;
using System.ComponentModel;

namespace Abeslamidze_Kursovaya7
{
    public partial class MainWindow : Window
    {
       
        private UnitOfWork unitOfWork = new UnitOfWork();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = ViewModel = new MainWindowViewModel(unitOfWork);

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
