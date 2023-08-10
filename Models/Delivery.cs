using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abeslamidze_Kursovaya7.Models
{
    public enum DeliveryStatus
    {
        InProgress,
        Done
    }
    public class Delivery
    {
        public Delivery(Location from, Location to, List<Guid> orderIds, Guid transportId)
        {
            Id = Guid.NewGuid();
            From = from;
            To = to;
            OrderIds = orderIds;
            TransportId = transportId;
            StartDate = DateTime.Now;
            Status = DeliveryStatus.InProgress;
        }

        public Guid Id { get; }

        public List<Guid> OrderIds { get; }
        public Guid TransportId { get; }
        public Location From { get; set; }
        public Location To { get; set; }    
        public double TotalPrice { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public DeliveryStatus Status { get; set; }


    }
}
