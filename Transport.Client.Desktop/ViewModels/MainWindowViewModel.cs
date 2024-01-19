using Abeslamidze_Kursovaya7.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Transport.DTOs;

namespace Abeslamidze_Kursovaya7.ViewModels
{
	public class MainWindowViewModel : ObservableObject
    {
        public bool DispatchInProgress = false;

        public List<LocationDto> AvailableLocations;
        public double MaxAvailableTransportVolume;

        private readonly ApiService _apiService;

        public MainWindowViewModel(ApiService a)
        {
			_apiService = a;
        }
       
        public LoginViewModel Login { get; } = new LoginViewModel();

        public ObservableCollection<OrderDto> Orders { get; } = new ObservableCollection<OrderDto>();

        public ObservableCollection<DeliveryDto> Deliveries { get; } = new ObservableCollection<DeliveryDto>();

        public ObservableCollection<TransportDto> Transports { get; } = new ObservableCollection<TransportDto>();

        public async Task AddNewOrder(NewOrderDto order)
        {
            await _apiService.CreateOrder(order);
			await UpdateState();
		}

        public async Task UpdateOrder(OrderDto order)
        {
			await _apiService.UpdateOrder(order);
		}

        public async Task DeleteOrder(OrderDto order)
        {
			await _apiService.DeleteOrder(order.Id);
		}

		public class DispatchServiceResult
		{
			public int NumOfNewDeliveries { get; set; }
			public int NumOfInProgressDeliveries { get; set; }
			public int NumOfDoneDeliveries { get; set; }
			public int NumOfAssignedOrders { get; set; }
			public int NumOfInQueueOrders { get; set; }
			public int NumOfInProgressOrders { get; set; }
			public int NumOfDoneOrders { get; set; }
			public int NumOfAssignedTransport { get; set; }
		}

		public async Task<DispatchServiceResult?> Dispatch()
        {
            var deliveries = await _apiService.DispatchDeliveries();
            if (deliveries.Count > 0)
            {
                var r = new DispatchServiceResult
                {
                    NumOfNewDeliveries = deliveries.Count,
                    // TODO fix
                    NumOfInQueueOrders = 0,
                    NumOfAssignedOrders = 0,
                    NumOfAssignedTransport = 0,
            };

				return r;
            }

            return null;
        }
        public async Task<DispatchServiceResult?> Start()
        {
			var deliveries = await _apiService.StartDeliveries();
			if (deliveries.Count > 0)
            {
                var r = new DispatchServiceResult 
                {
					NumOfInProgressDeliveries = deliveries.Count
				};

                return r;
            }

            return null;
            
        }

        public async Task Update(Guid deliveryId)
        {
			await _apiService.UpdateDelivery(deliveryId);
            return;

		}

        public async Task Initialize()
        {
			AvailableLocations = new List<LocationDto> { };

			var dbLocations = await _apiService.GetAllLocations();

			foreach (var location in dbLocations)
			{
				AvailableLocations.Add(new LocationDto { Id = location.Id, Name = location.Name });
			}

			MaxAvailableTransportVolume = await _apiService.GetTransportMaxVolume();

			await UpdateState();
        }

        public async Task UpdateState()
        {
            var orders = await _apiService.GetAllOrders();

            Orders.Clear();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }

            var deliveries = await _apiService.GetAllDeliveries();

			Deliveries.Clear();
            foreach (var delivery in deliveries)
            {
                Deliveries.Add(delivery);
            }

            var transports = await _apiService.GetAllTransports();

            Transports.Clear();
            foreach (var transport in transports)
            {
                Transports.Add(transport);
            }
        }
    }
}
