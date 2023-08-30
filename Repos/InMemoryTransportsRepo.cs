﻿using Abeslamidze_Kursovaya7.Interfaces;
using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abeslamidze_Kursovaya7.Repos
{
    public class InMemoryTransportsRepo : ITransportsRepo
    {
        private List<Transport> _transports = new List<Transport>
        {
           new Transport(350, 1500, 25),
           new Transport(550, 500, 15),
           new Transport(450, 1000, 35)
        };

        public void Update(Transport transport)
        {
            throw new NotImplementedException();
        }

        public List<Transport> GetAll()
        {
            return _transports;
        }

        public Transport? GetById(Guid id)
        {
            return _transports.FirstOrDefault(t => t.Id == id);
        }

        public List<Transport> GetInTransit()
        {
            return _transports
                .Where(t => t.Status == TransportStatus.InTransit)
                .OrderByDescending(t => t.Volume)
                .ToList(); ;
        }

        public List<Transport> GetFree()
        {
            return _transports
                .Where(t => t.Status == TransportStatus.Free)
                .OrderByDescending(t => t.Volume)
                .ToList(); ;
        }

        public double GetSpeedInKmById(Guid id)
        {
            return _transports
                .Where(t => t.Id == id)
                .Select(t => t.Speed)
                .First();
        }

        public double GetPricePerKmById(Guid id)
        {
            return _transports
                .Where(t => t.Id == id)
                .Select(t => t.PricePerKm)
                .First();
        }

        public double GetMaxVolume()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

    }



}
