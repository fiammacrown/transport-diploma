using System;
using System.Collections.Generic;
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
        private List<Order> _orders = new List<Order>();

        public Transport(double speed, double volume, double pricePerKm)
        {
            Id = Guid.NewGuid();
            Speed = speed;
            Volume = volume;
            PricePerKm = pricePerKm;
            Status = TransportStatus.Free;
        }

        public Guid Id { get; }
        public double Speed { get; }
        public double Volume { get; }
        public double AvailableVolume { get => Volume - _currentLoad; }
        public List<Guid> AssignedOrders { get => _orders.Select(o => o.Id).ToList(); }
        public double PricePerKm { get; }
        public TransportStatus Status { get; set; }


        public void Load(Order order)
        {
            _currentLoad += order.Weight;
            _orders.Add(order);
        }


    }
}
