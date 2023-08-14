using Abeslamidze_Kursovaya7.Repos;
using Abeslamidze_Kursovaya7.Models;
using Abeslamidze_Kursovaya7.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Abeslamidze_Kursovaya7.Interfaces;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly IOrdersRepo _ordersService;
        private readonly ITransportsRepo _transportsRepo;
        private readonly IDeliveriesRepo _deliveriesRepo;
        private readonly DispatchService _dispatchService;

        public MainWindowViewModel(IOrdersRepo ordersService, ITransportsRepo transportsRepo, IDeliveriesRepo deliveriesRepo)
        {
            _ordersService = ordersService;
            _transportsRepo = transportsRepo;
            _deliveriesRepo = deliveriesRepo;

            _dispatchService = new DispatchService(_ordersService, _transportsRepo, _deliveriesRepo);
        }

        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();
        public ObservableCollection<Transport> Transports { get; } = new ObservableCollection<Transport>();

        public void AddNewOrder(Order order)
        {
            Orders.Add(order);
            _ordersService.Add(order);
        }

        public DispatchServiceResult Calculate()
        {
           return _dispatchService.Dispatch();
        }

        public DispatchServiceResult Tick()
        {
            return _dispatchService.Update();
        }

        public void UpdateState()
        {
            var orders = _ordersService.GetAll();

            Orders.Clear();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }


            var transports = _transportsRepo.GetAll();

            Transports.Clear();
            foreach (var transport in transports)
            {
                Transports.Add(transport);
            }
        }
    }
}
