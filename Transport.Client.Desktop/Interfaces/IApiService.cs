using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transport.DTOs;

namespace Abeslamidze_Kursovaya7.Services
{
	public interface IApiService
	{
		// Login
		[Post("/Users/Login")]
		public Task<UserTokenDto> Login([Body] UserDto user);

		// Locations
		[Get("/Locations")]
		[Headers("Authorization: Bearer")]
		public Task<List<LocationDto>> GetAllLocations();

		// Transports
		[Get("/Transports")]
		[Headers("Authorization: Bearer")]
		public Task<List<TransportDto>> GetAllTransports();

		[Get("/Transports/GetMaxVolume")]
		[Headers("Authorization: Bearer")]
		public Task<double> GetTransportMaxVolume();

		// Deliveries
		[Get("/Deliveries")]
		[Headers("Authorization: Bearer")]
		public Task<List<DeliveryDto>> GetAllDeliveries();

		[Get("/Deliveries/Dispatch")]
		[Headers("Authorization: Bearer")]
		public Task<List<DeliveryDto>> DispatchDeliveries();

		[Get("/Deliveries/Start")]
		[Headers("Authorization: Bearer")]
		public Task<List<DeliveryDto>> StartDeliveries();

		[Get("/Deliveries/{id}")]
		[Headers("Authorization: Bearer")]
		public Task UpdateDelivery(Guid id);
		
		// Orders
		[Get("/Orders")]
		[Headers("Authorization: Bearer")]
		public Task<List<OrderDto>> GetAllOrders();

		[Get("/Orders/{id}")]
		[Headers("Authorization: Bearer")]
		public Task<OrderDto> GetOrder(Guid id);

		[Post("/Orders")]
		[Headers("Authorization: Bearer")]
		public Task<OrderDto> CreateOrder([Body] NewOrderDto order);

		[Put("/Orders/{id}")]
		[Headers("Authorization: Bearer")]
		public Task<OrderDto> UpdateOrder(Guid id, [Body] OrderDto order);

		[Delete("/Orders/{id}")]
		[Headers("Authorization: Bearer")]
		public Task DeleteOrder(Guid id);
	}
}
