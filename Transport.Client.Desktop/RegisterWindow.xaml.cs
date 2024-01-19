using System.Windows;
using Abeslamidze_Kursovaya7.ViewModels;
using Transport.DTOs;

namespace Abeslamidze_Kursovaya7
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {

            InitializeComponent();

            DataContext = ViewModel = new RegisterWindowViewModel()
            {
                CloseDelegate = (order) =>
                {
                    DataResult = order;
                    DialogResult = true;
                    Close();
                }
            };
        }

        public RegisterWindowViewModel ViewModel { get; }

        public NewOrderDto? DataResult { get; private set; }
    }
}
