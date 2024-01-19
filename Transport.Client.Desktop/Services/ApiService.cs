using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transport.DTOs;

namespace Abeslamidze_Kursovaya7.Services
{
	public class ApiService
	{
		private readonly IApiService _apiService;

		public ApiService()
		{
			_apiService = RestService.For<IApiService>("https://localhost:7284/");
		}

		public async Task<List<LocationDto>> GetAllLocations()
		{
			var locations = await _apiService.GetAllLocations();
			return locations;
		}

		public async Task<List<TransportDto>> GetAllTransports()
		{
			var transports = await _apiService.GetAllTransports();
			return transports;
		}

		public async Task<double> GetTransportMaxVolume()
		{
			var maxVolume = await _apiService.GetTransportMaxVolume();
			return maxVolume;
		}

		public async Task<List<DeliveryDto>> GetAllDeliveries()
		{
			var deliveries = await _apiService.GetAllDeliveries();
			return deliveries;
		}

		public async Task<List<DeliveryDto>> DispatchDeliveries()
		{
			var deliveries = await _apiService.DispatchDeliveries();
			return deliveries;
		}

		public async Task<List<DeliveryDto>> StartDeliveries()
		{
			var deliveries = await _apiService.StartDeliveries();
			return deliveries;
		}

		public async Task UpdateDelivery(Guid id)
		{
			await _apiService.UpdateDelivery(id);
			return;
		}

		public async Task<List<OrderDto>> GetAllOrders()
		{
			var orders = await _apiService.GetAllOrders();
			return orders;
		}

		public async Task<OrderDto> GetOrder(Guid id)
		{
			var order = await _apiService.GetOrder(id);
			return order;
		}

		public async Task<OrderDto> CreateOrder(NewOrderDto order)
		{
			var newOrder = await _apiService.CreateOrder(order);
			return newOrder;
		}

		public async Task<OrderDto> UpdateOrder(OrderDto order)
		{
			var newOrder = await _apiService.UpdateOrder(order.Id, order);
			return newOrder;
		}

		public async Task DeleteOrder(Guid id)
		{
			await _apiService.DeleteOrder(id);
			return;
		}
	}
}
