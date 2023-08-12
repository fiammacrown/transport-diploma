using System.Windows;
using Abeslamidze_Kursovaya7.ViewModels;
using Abeslamidze_Kursovaya7.Services;
using Abeslamidze_Kursovaya7.Models;

using System.Collections.Generic;

namespace Abeslamidze_Kursovaya7
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = ViewModel = new MainWindowViewModel();
        }

        public MainWindowViewModel ViewModel { get; }

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
            var message = string.Format("Распределение заявок выполнено!\nСформировано {0} грузоперевозок\n{1} заявок попало в очередь\nДоступно {2} единиц транспорта",
                result.NumOfInProgressDeliveries,
                result.NumOfInQueueOrders,
                result.NumOfFreeTransport );
            MessageBox.Show(message);
        }

        private void Run()
        {

        }
    }
}
