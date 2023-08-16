using Abeslamidze_Kursovaya7.Repos;
using Abeslamidze_Kursovaya7.Models;
using Abeslamidze_Kursovaya7.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        private readonly DispatchService _dispatchService;

        public MainWindowViewModel()
        {
            _dispatchService = new DispatchService(unitOfWork);
        }
       
        public LoginViewModel Login { get; } = new LoginViewModel();

        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();

        public ObservableCollection<Delivery> Deliveries { get; } = new ObservableCollection<Delivery>();
        public ObservableCollection<Transport> Transports { get; } = new ObservableCollection<Transport>();

        public void AddNewOrder(Order order)
        {
            Orders.Add(order);
            unitOfWork.OrderRepository.Add(order);
        }

        public DispatchServiceResult Calculate()
        {
           _dispatchService.Dispatch();
           _dispatchService.Start();

            return new DispatchServiceResult(
                unitOfWork.DeliveryRepository.GetInProgress(),
                unitOfWork.OrderRepository.GetInQueue(),
                unitOfWork.OrderRepository.GetDeliverableOrders(),
                unitOfWork.TransportRepository.GetFree()
                );
        }

        public DispatchServiceResult Tick()
        {
            _dispatchService.Update();

            return new DispatchServiceResult(
               unitOfWork.DeliveryRepository.GetInProgress(),
               unitOfWork.OrderRepository.GetInQueue(),
               unitOfWork.OrderRepository.GetDeliverableOrders(),
               unitOfWork.TransportRepository.GetFree()
               );
        }

        public void UpdateState()
        {
            var orders = unitOfWork.OrderRepository.GetAll();

            Orders.Clear();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }

            var deliveries = unitOfWork.DeliveryRepository.GetAll();

            Deliveries.Clear();
            foreach (var delivery in deliveries)
            {
                Deliveries.Add(delivery);
            }

            var transports = unitOfWork.TransportRepository.GetAll();

            Transports.Clear();
            foreach (var transport in transports)
            {
                Transports.Add(transport);
            }
        }
    }
}
