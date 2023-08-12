using Abeslamidze_Kursovaya7.Models;
using Abeslamidze_Kursovaya7.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();

        public DispatchServiceResult Calculate()
        {
            var orders = new List<Order>
            { 
                new Order(50, new Location("Брест"), new Location("Минск")),
                new Order(100, new Location("Брест"), new Location("Минск")),
                new Order(100, new Location("Минск"), new Location("Брест")),
            };
            var transports = new List<Transport>
            {
                new Transport(350, 1500, 25),
                //new Transport(350, 1500, 25),
                //new Transport(350, 1500, 25),
                //new Transport(350, 1500, 25),
                //new Transport(350, 1500, 25),

            };

            // calculate Delivery 
            // update data grid with delivery
            // put undelivered orders in queuq
            // show message with statistic: quantity of dispatched/ quantity of in queuq
           return new DispatchService(orders, transports).Dispatch();
        }
    }
}
