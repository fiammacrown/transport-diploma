using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private const string AdminPin = "boo";

        private bool _isAuthorized = false;
        private RelayCommand? _loginCommand;

        public bool IsAuthorized
        {
            get => _isAuthorized;
            private set => SetProperty(ref _isAuthorized, value);
        }

        public string Pin { get; set; }

        public RelayCommand LoginCommand => _loginCommand ??= new RelayCommand(Login);

        private void Login()
        {
            IsAuthorized = Pin == AdminPin;
        }
    }
}
