using Abeslamidze_Kursovaya7.Interfaces;
using Abeslamidze_Kursovaya7.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abeslamidze_Kursovaya7.Repos
{
    public class DatabaseLocationRepo : ILocationsRepo
    {
        private readonly EntityContext _entityContext;

        public DatabaseLocationRepo(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public List<Location> GetAll()
        {
            _entityContext.Locations.Load();
            return _entityContext.Locations.Local.ToList();
        }

        public Location? GetById(Guid id)
        {
            return _entityContext.Locations
                .FirstOrDefault(t => t.Id == id);
        }
    }
}
