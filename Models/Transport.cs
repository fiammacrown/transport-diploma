using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        private List<Order> _assignedOrders = new List<Order>();

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
        [NotMapped]
        public double AvailableVolume { get => Volume - _currentLoad; }
        [NotMapped]
        public List<Guid> AssignedOrders { get => _assignedOrders.Select(o => o.Id).ToList(); }
        public double PricePerKm { get; set; }
        public TransportStatus Status { get; set; }


        public void Load(Order order)
        {
            _currentLoad += order.Weight;
            _assignedOrders.Add(order);
        }

        public void Unload(Order order)
        {
           _currentLoad -= order.Weight;
           _assignedOrders.Remove(order);
        }

    }
}
