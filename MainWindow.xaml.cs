using System.Windows;
using Abeslamidze_Kursovaya7.ViewModels;
using Abeslamidze_Kursovaya7.Services;
using Abeslamidze_Kursovaya7.Models;
using Abeslamidze_Kursovaya7.Repos;

using System.Collections.Generic;
using System;

namespace Abeslamidze_Kursovaya7
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = ViewModel = new MainWindowViewModel(new OrdersRepo(), new TransportsRepo(), new DeliveriesRepo());
        }

        public MainWindowViewModel ViewModel { get; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateState();

            // start runner
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();

            if (registerWindow.ShowDialog() == true)
            {
                var result = registerWindow.DataResult;
                if (result != null)
                {
                    ViewModel.Orders.Add(result);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var result = ViewModel.Calculate();
            var message = string.Format("Сформировано грузоперевозок: {0}\nЗаявки в очереди: {1}\nДоступно единиц транспорта: {2}",
                result.NumOfInProgressDeliveries,
                result.NumOfInQueueOrders,
                result.NumOfFreeTransport );
            MessageBox.Show(message, "Распределение заявок выполнено!");

            ViewModel.UpdateState();
            Button_Dispatch.IsEnabled = false;
        }

        private void Run()
        {
            
            
            // create new thread with Runner 
            // run on UI thread
            this.Dispatcher.Invoke(() => { 
                ViewModel.UpdateState();
            });
        }
    }
}
