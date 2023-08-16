using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private const string AdminPin = "boo";

#if DEBUG
        private bool _isAuthorized = true;
#else
        private bool _isAuthorized = false;
#endif
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
