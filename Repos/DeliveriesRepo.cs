using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abeslamidze_Kursovaya7.Repos
{
    public class DeliveriesRepo
    {
        private List<Delivery> _deliveries = new List<Delivery>();

        public List<Delivery> GetAll()
        {
            return _deliveries;
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
