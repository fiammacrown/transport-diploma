using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Abeslamidze_Kursovaya7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //dGrid.ItemsSource = new ObservableCollection<Order>
            //{
            //    new Order(),
            //    new Order(),
            //    new Order(),
            //    new Order(),
            //    new Order(),
            //    new Order(),
            //    new Order(),
            //    new Order(),
            //};
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();

            if (registerWindow.ShowDialog() == false)
            {
                return;
            }

            var model = registerWindow.Model;

            //((ObservableCollection<Order>)dGrid.ItemsSource).Add(new Order());
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
