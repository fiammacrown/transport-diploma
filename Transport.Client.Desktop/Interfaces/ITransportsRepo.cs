
using System;
using System.Collections.Generic;
using Transport.DTOs;
//using Abeslamidze_Kursovaya7.Models;

namespace Abeslamidze_Kursovaya7.Interfaces
{

	public interface ITransportsRepo
    {
        void Update(TransportDto transport);
        List<TransportDto> GetAll();
		TransportDto? GetById(Guid id);
        List<TransportDto> GetFree();
        List<TransportDto> GetInTransit();
        double GetPricePerKmById(Guid id);
        double GetSpeedInKmById(Guid id);
        double GetMaxVolume();
    }
}