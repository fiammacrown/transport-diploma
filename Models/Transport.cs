using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Linq;

namespace Abeslamidze_Kursovaya7.Models
{

    public enum TransportStatus
    {
        InTransit,
        Free
    }

    public class Transport
    {
        private double _currentLoad = 0;

        public Transport()
        {
        }

        public Transport(double speed, double volume, double pricePerKm)
        {
            Id = Guid.NewGuid();
            Speed = speed;
            Volume = volume;
            PricePerKm = pricePerKm;
            Status = TransportStatus.Free;
        }

        public Guid Id { get; set; }
        public double Speed { get; set; }
        public double Volume { get; set; }
        public double AvailableVolume { get => Volume - _currentLoad; set { } }
        public double PricePerKm { get; set; }
        public TransportStatus Status { get; set; }


        public void Load(Order order)
        {
            if ((_currentLoad + order.Weight) <= AvailableVolume)
            {
                _currentLoad += order.Weight;
            }
            else
            {
                throw new Exception("Invalid operation: Transport.Load");
            };
        }

        public void Unload(Order order)
        {
            if ((_currentLoad - order.Weight) >= 0)
            { 
                _currentLoad -= order.Weight;
            }
            else
            {
                throw new Exception("Invalid operation: Transport.Unload");
            };
        }

        public void Done()
        {
            Status = TransportStatus.Free;
        }

        public void InTransit()
        {
            Status = TransportStatus.InTransit;
        }

        public override string? ToString()
        {
            return Id.ToString();
        }

    }
}
