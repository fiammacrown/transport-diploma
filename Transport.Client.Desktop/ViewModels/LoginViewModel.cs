using Abeslamidze_Kursovaya7.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly AuthService _authService;

		private bool _isAuthorized = false;
        private string _errorMessage = string.Empty;
        private RelayCommand? _loginCommand;
        private RelayCommand? _logoutCommand;

        public LoginViewModel(AuthService authService)
        {
			_authService = authService;
		}

        public bool IsAuthorized
        {
            get => _isAuthorized;
            private set => SetProperty(ref _isAuthorized, value);
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public string ErrorMessage
        {
            get => _errorMessage;
            private set => SetProperty(ref _errorMessage, value);
        }

        public RelayCommand LoginCommand => _loginCommand ??= new RelayCommand(Login);

        public RelayCommand LogoutCommand => _logoutCommand ??= new RelayCommand(Logout);

        private async void Login()
        {
			IsAuthorized = await _authService.LoginAsync(Username, Password);

			ErrorMessage = IsAuthorized
                ? string.Empty
                : "Введенный пароль неверен, попробуйте другой";
		}

        private void Logout()
        {
            _authService.Logout();

            IsAuthorized = false;
			Password = string.Empty;
			ErrorMessage = string.Empty;
		}
    }
}
