using Abeslamidze_Kursovaya7.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();
    }
}
