using System.Windows;
using Abeslamidze_Kursovaya7.ViewModels;

namespace Abeslamidze_Kursovaya7
{

    public partial class RegisterWindow : Window
    {
        private RegisterWindowViewModel _viewModel;

        public RegisterWindowViewModel Model => _viewModel;

        public RegisterWindow()
        {
            InitializeComponent();

            _viewModel = new RegisterWindowViewModel();
            DataContext = _viewModel;
        }
    }
}
