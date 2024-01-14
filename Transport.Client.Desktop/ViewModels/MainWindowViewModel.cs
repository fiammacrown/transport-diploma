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

        public List<Location> AvailableLocations;
        public double MaxAvailableTransportVolume;

        private readonly UnitOfWork _unitOfWork;

        public MainWindowViewModel(UnitOfWork u)
        {
            _unitOfWork = u;
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

        public void UpdateOrder(Order order) 
        {
            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.Save();

        }

        public void DeleteOrder(Order order)
        {
            _unitOfWork.OrderRepository.Delete(order);
            _unitOfWork.Save();

        }

        public DispatchServiceResult? Dispatch()
        {
            var d = new DispatchService(_unitOfWork);
            if (d.Dispatch())
            {
                _unitOfWork.Save();

                var r = new DispatchServiceResult();

                r.NumOfNewDeliveries = d.NumOfNewDeliveries;
                r.NumOfInQueueOrders = d.NumOfInQueueOrders;
                r.NumOfAssignedOrders = d.NumOfAssignedOrders;
                r.NumOfAssignedTransport = d.NumOfAssignedTransport;

                return r;
            }

            return null;
        }
        public DispatchServiceResult? Start()
        {
            var d = new DispatchService(_unitOfWork);
            if (d.Start())
            {
                _unitOfWork.Save();

                var r = new DispatchServiceResult();

                r.NumOfInProgressDeliveries = d.NumOfInProgressDeliveries;

                return r;
            }

            return null;
            
        }

        public DispatchServiceResult? Update()
        {
            var d = new DispatchService(_unitOfWork);
            if (d.Update())
            {
                _unitOfWork.Save();

                var r = new DispatchServiceResult();

                r.NumOfDoneDeliveries = d.NumOfDoneDeliveries;

                return r;
            }

            return null;
        }

        public void Initialize()
        {
            
            AvailableLocations = _unitOfWork.LocationRepository.GetAll();
            MaxAvailableTransportVolume = _unitOfWork.TransportRepository.GetMaxVolume();

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
