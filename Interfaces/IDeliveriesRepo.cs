using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;

namespace Abeslamidze_Kursovaya7.Interfaces
{
    public interface IDeliveriesRepo
    {
        void Add(Delivery delivery);
        List<Delivery> GetAll();
        Delivery? GetById(Guid id);
        List<Delivery> GetInProgress();
    }
}