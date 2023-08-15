using Abeslamidze_Kursovaya7.Interfaces;
using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abeslamidze_Kursovaya7.Repos
{
    public class InMemoryDeliveriesRepo : IDeliveriesRepo
    {
        private List<Delivery> _deliveries = new List<Delivery>();

        public void Update(Delivery delivery)
        {
            throw new NotImplementedException();
        }
        public List<Delivery> GetAll()
        {
            return _deliveries;
        }

        public List<Delivery> GetInProgress()
        {
            return _deliveries
                .Where(d => d.Status == DeliveryStatus.InProgress)
                .ToList();
        }

        public Delivery? GetById(Guid id)
        {
            return _deliveries.FirstOrDefault(d => d.Id == id);
        }

        public void Add(Delivery delivery)
        {
            _deliveries.Add(delivery);
        }
    }
}
