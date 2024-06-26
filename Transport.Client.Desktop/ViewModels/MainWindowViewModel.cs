﻿using Abeslamidze_Kursovaya7.Models;
using Abeslamidze_Kursovaya7.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Transport.Client.Desktop.Models;
using Transport.DTOs;

namespace Abeslamidze_Kursovaya7.ViewModels
{
	public class MainWindowViewModel : ObservableObject
    {
        public List<LocationDto> AvailableLocations;
        public double MaxAvailableTransportVolume;

        private readonly IApiService _apiService;

        public MainWindowViewModel(IApiService apiService, AuthService authService)
        {
			_apiService = apiService;

            Login = new LoginViewModel(authService);
			Login.PropertyChanged += _Login_PropertyChanged;
		}

		private async void _Login_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(Login.IsAuthorized))
			{
				if (Login.IsAuthorized)
				{
                    await _apiService.UpdateAllDelivery();
					await Initialize();
				}
			}
		}

		public LoginViewModel Login { get; }
        public string UserToken { get; }

        public ObservableCollection<OrderModel> Orders { get; } = new ObservableCollection<OrderModel>();

        public ObservableCollection<DeliveryModel> Deliveries { get; } = new ObservableCollection<DeliveryModel>();

        public ObservableCollection<TransportDto> Transports { get; } = new ObservableCollection<TransportDto>();


		public async Task AddNewUser(UserDto user)
		{
			await _apiService.Register(user);
		}

		public async Task AddNewOrder(NewOrderDto order)
        {
            await _apiService.CreateOrder(order);
			await UpdateState();
		}

        public async Task UpdateOrder(OrderModel order)
        {
			await _apiService.UpdateOrder(order.Id, order);
		}

        public async Task DeleteOrder(OrderModel order)
        {
			await _apiService.DeleteOrder(order.Id);
		}

		public async Task<List<DeliveryDto>?> Dispatch()
        {
            var deliveries = await _apiService.DispatchDeliveries();
            if (deliveries.Count < 0)
            {
                return null;
            }
			return deliveries;
		}
        public async Task<List<DeliveryDto>?> Start()
        {
			var deliveries = await _apiService.StartDeliveries();
			if (deliveries.Count < 0)
			{
				return null;
			}
			return deliveries;
            
        }

        public async Task Update(Guid deliveryId)
        {
			await _apiService.UpdateDelivery(deliveryId);
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
            var orders = (await _apiService.GetAllOrders()).Select(x => OrderModel.Map(x));

            Orders.Clear();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }

            var deliveries = (await _apiService.GetAllDeliveries()).Select(x => DeliveryModel.Map(x));

			//Deliveries.Clear();
            foreach (var delivery in deliveries)
            {
                var existingDelivery = Deliveries.FirstOrDefault(d => d.Id == delivery.Id);
				if (existingDelivery != null)
                {
                    existingDelivery.StartDate = delivery.StartDate;
                    existingDelivery.EndDate = delivery.EndDate;
                    existingDelivery.Price  = delivery.Price;   
                    existingDelivery.Status = delivery.Status;
                }
                else
                {
					Deliveries.Insert(0, delivery);
				}  
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
