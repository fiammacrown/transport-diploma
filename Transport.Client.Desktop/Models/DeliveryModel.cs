using Abeslamidze_Kursovaya7.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Transport.DTOs;

namespace Transport.Client.Desktop.Models
{
    public class DeliveryModel: ObservableObject
	{
		double _progress = 0;
		DateTime? _startDate = null;
		DateTime? _endDate = null;
		string _status = string.Empty;

		public Guid Id { get; set; }

		public OrderDto Order { get; set; }

		public TransportDto Transport { get; set; }

		public DateTime? StartDate
		{
			get => _startDate;
			set => SetProperty(ref _startDate, value);
		}

		public DateTime? EndDate
		{
			get => _endDate;
			set => SetProperty(ref _endDate, value);
		}

		public double? Price { get; set; }

		public string Status
		{
			get => _status;
			set => SetProperty(ref _status, value);
		}

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
