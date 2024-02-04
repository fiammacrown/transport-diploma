using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Transport.DTOs;

namespace Abeslamidze_Kursovaya7.ViewModels
{
	public class CreateUserViewModel: ObservableObject, IDataErrorInfo
	{
		private string _username = string.Empty;
		private string _password = string.Empty;

		private readonly RelayCommand _saveCommand;
		private readonly RelayCommand _cancelCommand;

		public CreateUserViewModel()
		{
			_saveCommand = new RelayCommand(Save, CanSave);
			_cancelCommand = new RelayCommand(Cancel);

		}

		public Action<UserDto?>? CloseDelegate { get; set; }



		public string this[string columnName]
		{
			get
			{
				return Validate(columnName);
			}
		}


		public string Username
		{
			get => _username;
			set
			{
				if (SetProperty(ref _username, value))
				{
					_saveCommand.NotifyCanExecuteChanged();
				}
			}
		}

		public string Password
		{

			get => _password;
			set
			{
				if (SetProperty(ref _password, value))
				{
					_saveCommand.NotifyCanExecuteChanged();
				}
			}
		}

		public ICommand SaveCommand
		{
			get => _saveCommand;
		}

		public ICommand CancelCommand
		{
			get => _cancelCommand;
		}

		public string Error { get { return "Ошибка ввода!"; } }

		private string Validate(string propertyName)
		{
			string validationMessage = string.Empty;
			switch (propertyName)
			{
				case nameof(Password):
					if (Password.Length < 6)
					{
						validationMessage = "Пароль должен быть не меньше 6 символов";
					}
					break;
			}

			return validationMessage;
		}

		private void Save()
		{
			var user = new UserDto { Username = _username, Password = _password };


			CloseDelegate?.Invoke(user);
		}

		private bool CanSave()
		{
			var isValid = string.IsNullOrEmpty(Validate(nameof(Username)))
				&& string.IsNullOrEmpty(Validate(nameof(Password)));

			return isValid;
		}

		private void Cancel()
		{
			CloseDelegate?.Invoke(null);
		}
	}
}
