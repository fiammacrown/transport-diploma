using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;

namespace Abeslamidze_Kursovaya7.Interfaces
{
    public interface ITransportsRepo
    {
        void Update(Transport transport);
        List<Transport> GetAll();
        Transport? GetById(Guid id);
        List<Transport> GetFree();
        double GetPricePerKmById(Guid id);
        double GetSpeedInKmById(Guid id);
        double GetMaxVolume();
    }
}