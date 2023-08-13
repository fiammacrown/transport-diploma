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


        public List<Transport> GetAll()
        {
            _entityContext.Transports.Load();
            return _entityContext.Transports.Local.ToList();
        }

        public Transport? GetById(Guid id)
        {
            //return _transports.FirstOrDefault(t => t.Id == id);
            return null;
        }


        public List<Transport> GetFree()
        {
            //return _transports
            //    .Where(t => t.Status == TransportStatus.Free)
            //    .Where(t => t.AssignedOrders.Count == 0)
            //    .OrderByDescending(t => t.Volume)
            //    .ToList(); ;
            return new List<Transport>();
        }

        public double GetSpeedInKmById(Guid id)
        {
            //return _transports
            //    .Where(t => t.Id == id)
            //    .Select(t => t.Speed)
            //    .First();
            return 0;
        }

        public double GetPricePerKmById(Guid id)
        {
            //return _transports
            //    .Where(t => t.Id == id)
            //    .Select(t => t.PricePerKm)
            //    .First();
            return 0;
        }
    }



}
