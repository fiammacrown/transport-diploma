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
            // TODO:
        }

        public List<Delivery> GetAll()
        {
            // TODO: check
            _entityContext.Deliveries.Load();
            return _entityContext.Deliveries.Local.ToList();
        }

        public Delivery? GetById(Guid id)
        {
            // TODO:
            return null;
        }

        public List<Delivery> GetInProgress()
        {
            // TODO:
            return new List<Delivery>();
        }
    }
}
