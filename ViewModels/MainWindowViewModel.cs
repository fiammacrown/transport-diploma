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

        public DispatchServiceResult Dispatch()
        {

           var result = new DispatchService(_unitOfWork).Dispatch();
           new DispatchService(_unitOfWork).Start();

            return result;
        }

        public DispatchServiceResult Update()
        {

            var result = new DispatchService(_unitOfWork).Update();

            return result;
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
