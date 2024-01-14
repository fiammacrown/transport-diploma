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
        public Delivery()
        {
        }

        public Delivery(Distance distance, Guid orderId, Guid transportId)
        {
            Id = Guid.NewGuid();
            TransportId = transportId;
            OrderId = orderId;
            Distance = distance;

            StartDate = null;
            EndDate = null;
            Price = null;
            Status = DeliveryStatus.New;
        }

        public Guid Id { get; set;  }
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
       
        public Guid TransportId { get; set; }
        [ForeignKey("TransportId")]
        public Transport Transport { get; set; }

        [NotMapped]
        public Distance Distance { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Price { get; set; }
        public DeliveryStatus Status { get; set; }

        public void CalculatePrice(Transport transport)
        {
            Price = Distance.InKm * transport.PricePerKm;
        }

        public void InProgress(Distance distance, DateTime date)
        {
            StartDate = date;
            EndDate = StartDate?.AddMinutes(distance.InKm / Transport!.Speed);
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
