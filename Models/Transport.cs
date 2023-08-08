using System;

namespace Abeslamidze_Kursovaya7.Models
{

    public enum TransportStatus
    {
        InTransit,
        Free
    }

    public class Transport
    {
        private double _currentLoad;
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
        public double PricePerKm { get; }
        public TransportStatus Status { get; set; }


    }
}
