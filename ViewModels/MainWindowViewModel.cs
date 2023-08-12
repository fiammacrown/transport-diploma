using Abeslamidze_Kursovaya7.Repos;
using Abeslamidze_Kursovaya7.Models;
using Abeslamidze_Kursovaya7.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly OrdersRepo _ordersService;
        private readonly TransportsRepo _transportsRepo;
        private readonly DeliveriesRepo _deliveriesRepo;

        public MainWindowViewModel(OrdersRepo ordersService, TransportsRepo transportsRepo, DeliveriesRepo deliveriesRepo)
        {
            _ordersService = ordersService;
            _transportsRepo = transportsRepo;
            _deliveriesRepo = deliveriesRepo;

        }

        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();

        public DispatchServiceResult Calculate()
        {
           return new DispatchService(_ordersService, _transportsRepo, _deliveriesRepo).Dispatch();
        }

        public DispatchServiceResult Tick()
        {
            return new DispatchService(_ordersService, _transportsRepo, _deliveriesRepo).Update();
        }

        public void UpdateState()
        {
            var orders = _ordersService.GetAll();

            Orders.Clear();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }
    }
}
