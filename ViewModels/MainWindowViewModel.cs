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
        public bool DispatchInProgress = false;

        private readonly UnitOfWork _unitOfWork;
        private readonly UnitOfWork _unitOfWorkBackground;
        private readonly DispatchService _dispatchService;
        //private readonly DispatchService _dispatchServiceBackground;

        public MainWindowViewModel(UnitOfWork u, UnitOfWork ub)
        {
            _unitOfWork = u;
            _unitOfWorkBackground = ub;
            _dispatchService = new DispatchService(u);
            //_dispatchServiceBackground = new DispatchService(ub);
        }
       
        public LoginViewModel Login { get; } = new LoginViewModel();

        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();

        public ObservableCollection<Delivery> Deliveries { get; } = new ObservableCollection<Delivery>();
        public ObservableCollection<Transport> Transports { get; } = new ObservableCollection<Transport>();

        public void AddNewOrder(Order order)
        {
            Orders.Add(order);

            _unitOfWork.OrderRepository.Add(order);
            _unitOfWork.Save();
                
        }

        public DispatchServiceResult Dispatch()
        {

           _dispatchService.Dispatch();
           _dispatchService.Start();

            return new DispatchServiceResult(
                _unitOfWork.DeliveryRepository.GetInProgress(),
                _unitOfWork.OrderRepository.GetInQueue(),
                _unitOfWork.OrderRepository.GetDeliverableOrders(),
                _unitOfWork.TransportRepository.GetFree()
                );
        }

        public DispatchServiceResult Update()
        {
            // загружаем в бэкграунд контекст актуальные данные
            //_unitOfWorkBackground.OrderRepository.GetAll();
            //_unitOfWorkBackground.DeliveryRepository.GetAll();
            //_unitOfWorkBackground.TransportRepository.GetAll();

            _dispatchService.Update();

            return new DispatchServiceResult(
               _unitOfWork.DeliveryRepository.GetInProgress(),
               _unitOfWork.OrderRepository.GetInQueue(),
               _unitOfWork.OrderRepository.GetDeliverableOrders(),
               _unitOfWork.TransportRepository.GetFree()
               );
        }

        public void Initialize()
        {
            _unitOfWork.LocationRepository.GetAll();
            UpdateState();
        }

        public void UpdateState()
        {
            var orders = _unitOfWork.OrderRepository.GetAll();

            Orders.Clear();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }

            var deliveries = _unitOfWork.DeliveryRepository.GetAll();

            Deliveries.Clear();
            foreach (var delivery in deliveries)
            {
                Deliveries.Add(delivery);
            }

            var transports = _unitOfWork.TransportRepository.GetAll();

            Transports.Clear();
            foreach (var transport in transports)
            {
                Transports.Add(transport);
            }
        }
    }
}
