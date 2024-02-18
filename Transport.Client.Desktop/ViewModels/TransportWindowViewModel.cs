using Abeslamidze_Kursovaya7.Models;
using Abeslamidze_Kursovaya7.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Transport.DTOs;

namespace Transport.Client.Desktop.ViewModels
{
    class TransportWindowViewModel 
    {
		private readonly IApiService _apiService;
		private readonly TransportDto _transport;
		public TransportWindowViewModel(TransportDto transport, IApiService apiService)
        {
			_apiService = apiService;
			_transport = transport;

			Name = transport.Name;
			Speed = transport.Speed;
			Volume = transport.Volume;
			ImageURL = transport.ImageURL;
			PricePerKm = transport.PricePerKm;
		}

		public string Name { get; set; }
		public double Speed { get; set; }
		public double Volume { get; set; }
		public double PricePerKm { get; set; }
		public string ImageURL { get; set; }
		public ObservableCollection<TransportHistoryRecord> Records { get; } = new ObservableCollection<TransportHistoryRecord>();

		public async void LoadData()
		{

			var transportHistory = await _apiService.GetTransportHistory(_transport.Id);
			foreach (var record in transportHistory.Records)
			{
				Records.Add(record);
			};
		}
	}
}
