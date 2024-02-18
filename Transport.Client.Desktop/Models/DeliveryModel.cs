using Abeslamidze_Kursovaya7.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Transport.DTOs;

namespace Transport.Client.Desktop.Models
{
    public class DeliveryModel: ObservableObject
	{
		double _progress = 0;

		public Guid Id { get; set; }
		public OrderDto Order { get; set; }
		public TransportDto Transport { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public double? Price { get; set; }
		public string Status { get; set; }

		public double Progress
		{
			get => _progress;
			set => SetProperty(ref _progress, value);
		}

		public static DeliveryModel Map(DeliveryDto dto)
		{
			return new DeliveryModel
			{
				Id = dto.Id,
				Order = dto.Order,
				Transport = dto.Transport,
				StartDate = dto.StartDate,
				EndDate = dto.EndDate,
				Price = dto.Price,
				Status = dto.Status,
				Progress = dto.Status == "Done" ? 100 : 0
			};
		}

	}
}
