using Abeslamidze_Kursovaya7.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Abeslamidze_Kursovaya7
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            
            IsVisibleChanged += UserControl_IsVisibleChanged;
        }

        // Wordaround: clear password box
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                if ((bool)e.NewValue) // visible
                {
                }
                else // hidden
                {
                    pwrd.Clear();
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pb && DataContext is LoginViewModel vm)
            {
                vm.Password = pb.Password;
            }
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e) // login by enter
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                e.Handled = true;

                if (sender is PasswordBox pb && DataContext is LoginViewModel vm)
                {
                    vm.Password = pb.Password;
                    vm.LoginCommand.Execute(null);
                }
            }
        }
    }
}
