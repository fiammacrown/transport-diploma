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
            AssignedOrders = 0;
        }

        public Guid Id { get; set; }
        public double Speed { get; set; }
        public double Volume { get; set; }
        public double AvailableVolume { get => Volume - _currentLoad; set { } }
        public int AssignedOrders { get; set; }
        public double PricePerKm { get; set; }
        public TransportStatus Status { get; set; }


        public void Load(Order order)
        {
            _currentLoad += order.Weight;
            AssignedOrders += 1;
        }

        public void Unload(Order order)
        {
           _currentLoad -= order.Weight;
            AssignedOrders -= 1;
        }

        public override string? ToString()
        {
            return Id.ToString();
        }

    }
}
