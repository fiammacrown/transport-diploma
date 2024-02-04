using System.Windows;
using Abeslamidze_Kursovaya7.ViewModels;
using Transport.DTOs;

namespace Abeslamidze_Kursovaya7
{
	public partial class CreateUserWindow : Window
	{
		public CreateUserWindow()
		{
			InitializeComponent();
			DataContext = ViewModel = new CreateUserViewModel()
			{
				CloseDelegate = (user) =>
				{
					DataResult = user;
					DialogResult = true;
					Close();
				}
			};
		}

		public CreateUserViewModel ViewModel { get; }

		public UserDto? DataResult { get; private set; }
	}
}
