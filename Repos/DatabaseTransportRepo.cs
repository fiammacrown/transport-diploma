using Abeslamidze_Kursovaya7.Interfaces;
using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Abeslamidze_Kursovaya7.Repos
{
    public class DatabaseTransportsRepo : ITransportsRepo
    {
        private readonly EntityContext _entityContext;

        public DatabaseTransportsRepo(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public void Update(Transport updated)
        {
            var existing = GetById(updated.Id);
            if (existing != null)
            {
                _entityContext.Entry(existing).CurrentValues.SetValues(updated);
                _entityContext.SaveChanges();
            }
        }

        public List<Transport> GetAll()
        {
            _entityContext.Transports.Load();
            return _entityContext.Transports.Local.ToList();
        }

        public Transport? GetById(Guid id)
        {
            return _entityContext.Transports
                .FirstOrDefault(t => t.Id == id);
        }


        public List<Transport> GetFree()
        {
            return _entityContext.Transports
                .Where(t => t.Status == TransportStatus.Free)
                .Where(t => t.AssignedOrders == 0)
                .OrderByDescending(t => t.Volume)
                .ToList(); ;
        }

        public double GetSpeedInKmById(Guid id)
        {
            return _entityContext.Transports
                .Where(t => t.Id == id)
                .Select(t => t.Speed)
                .First();
        }

        public double GetPricePerKmById(Guid id)
        {
            return _entityContext.Transports
                .Where(t => t.Id == id)
                .Select(t => t.PricePerKm)
                .First();
        }

        public double GetMaxVolume()
        {
            return _entityContext.Transports
                .Select(t => t.Volume)
                .Max();
        }
    }



}
