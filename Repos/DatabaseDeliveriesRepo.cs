using Abeslamidze_Kursovaya7.Interfaces;
using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Abeslamidze_Kursovaya7.Repos
{
    public class DatabaseDeliveriesRepo : IDeliveriesRepo
    {
        private readonly EntityContext _entityContext;

        public DatabaseDeliveriesRepo(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public void Add(Delivery delivery)
        {
            _entityContext.Deliveries.Add(delivery);
            _entityContext.SaveChanges();
        }

        public void Update(Delivery updated)
        {
            var existing = GetById(updated.Id);
            if (existing != null)
            {
                _entityContext.Entry(existing).CurrentValues.SetValues(updated);
                _entityContext.SaveChanges();
            }
        }
        public List<Delivery> GetAll()
        {
            _entityContext.Deliveries.Load();
            return _entityContext.Deliveries.Local.ToList();
        }

        public Delivery? GetById(Guid id)
        {
            return _entityContext.Deliveries
                .FirstOrDefault(d => d.Id == id); ;
        }

        public List<Delivery> GetInProgress()
        {
            return _entityContext.Deliveries
                .Where(d => d.Status == DeliveryStatus.InProgress)
                .ToList();
        }
    }
}
