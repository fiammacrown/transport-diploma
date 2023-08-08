using System.Windows;
using Abeslamidze_Kursovaya7.ViewModels;
using Abeslamidze_Kursovaya7.Models;

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
            // calculate Delivery 
            // update data grid with delivery
            // put undelivered orders in queuq
            // show message with statistic: quantity of dispatched/ quantity of in queuq
        }
    }
}
