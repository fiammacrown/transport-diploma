using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private const string AdminPassword = "admin";

        private bool _isAuthorized = false;
        private string _errorMessage = string.Empty;
        private RelayCommand? _loginCommand;
        private RelayCommand? _logoutCommand;

        public bool IsAuthorized
        {
            get => _isAuthorized;
            private set => SetProperty(ref _isAuthorized, value);
        }

        public string Password { get; set; }

        public string ErrorMessage
        {
            get => _errorMessage;
            private set => SetProperty(ref _errorMessage, value);
        }

        public RelayCommand LoginCommand => _loginCommand ??= new RelayCommand(Login);

        public RelayCommand LogoutCommand => _logoutCommand ??= new RelayCommand(Logout);

        private void Login()
        {
            IsAuthorized = Password == AdminPassword;
            ErrorMessage = IsAuthorized ? string.Empty : "Введенный пароль неверен, попробуйте другой";
        }

        private void Logout()
        {
            IsAuthorized = false;
            Password = string.Empty;
            ErrorMessage = string.Empty;
        }
    }
}
