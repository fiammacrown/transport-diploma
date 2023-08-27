using System.Windows;
using Abeslamidze_Kursovaya7.Interfaces;
using Abeslamidze_Kursovaya7.Models;
using Abeslamidze_Kursovaya7.ViewModels;

namespace Abeslamidze_Kursovaya7
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow(UnitOfWork u)
        {

            InitializeComponent();

            DataContext = ViewModel = new RegisterWindowViewModel(u)
            {
                CloseDelegate = (order) =>
                {
                    DataResult = order;
                    DialogResult = true;
                    Close();
                }
            };
        }

        private RegisterWindowViewModel ViewModel { get; }

        public Order? DataResult { get; private set; }
    }
}
