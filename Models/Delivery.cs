using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abeslamidze_Kursovaya7.Models
{
    public enum DeliveryStatus
    {
        New,
        InProgress,
        Done
    }
    public class Delivery
    {
        private Distance _distance;
        public Delivery()
        {
        }

        public Delivery(Location from, Location to,  Order order, Transport transport)
        {
            Id = Guid.NewGuid();
            Transport = transport;
            Order = order;

            From = from;
            To = to;

            _distance = new Distance(From, To);
           
            StartDate = null;
            EndDate = StartDate?.AddMinutes(_distance.InKm / Transport.Speed);
            Price = _distance.InKm * Transport.PricePerKm;
            Status = DeliveryStatus.New;
        }

        public Guid Id { get; set;  }
        public Order Order { get; set; }
        public Transport Transport { get; set; }
        public Location From { get; set; }
        public Location To { get; set; }    
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Price { get; set; }
        public DeliveryStatus Status { get; set; }

        public void InProgress(DateTime date)
        {
            StartDate = date;
            Status = DeliveryStatus.InProgress;
        }

        public void Done()
        {
            Status = DeliveryStatus.Done;
        }

        public override string? ToString()
        {
            return Id.ToString();
        }
    }
}
