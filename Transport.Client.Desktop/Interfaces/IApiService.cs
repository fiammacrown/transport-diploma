using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transport.DTOs;

namespace Abeslamidze_Kursovaya7.Services
{
	internal interface IApiService
	{
		// Locations
		[Get("/Locations")]
		public Task<List<LocationDto>> GetAllLocations();

		// Transports
		[Get("/Transports")]
		public Task<List<TransportDto>> GetAllTransports();

		[Get("/Transports/GetMaxVolume")]
		public Task<double> GetTransportMaxVolume();

		// Deliveries
		[Get("/Deliveries")]
		public Task<List<DeliveryDto>> GetAllDeliveries();

		[Get("/Deliveries/Dispatch")]
		public Task<List<DeliveryDto>> DispatchDeliveries();

		[Get("/Deliveries/Start")]
		public Task<List<DeliveryDto>> StartDeliveries();

		[Get("/Deliveries/{id}")]
		public Task UpdateDelivery(Guid id);
		// Orders
		[Get("/Orders")]
		public Task<List<OrderDto>> GetAllOrders();
		[Get("/Orders/{id}")]
		public Task<OrderDto> GetOrder(Guid id);
		[Post("/Orders")]
		public Task<OrderDto> CreateOrder([Body] NewOrderDto order);
		[Put("/Orders/{id}")]
		public Task<OrderDto> UpdateOrder(Guid id, [Body] OrderDto order);
		[Delete("/Orders/{id}")]
		public Task DeleteOrder(Guid id);

	}
}
