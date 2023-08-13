using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Delivery()
        {
        }

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

        public Guid Id { get; set;  }

        public List<Guid> OrderIds { get; set; }
        public Guid TransportId { get; set; }
        [NotMapped]
        public Location From { get; set; }
        [NotMapped]
        public Location To { get; set; }    
        public double TotalPrice { get; set; }
        public double TotalWeight { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DeliveryStatus Status { get; set; }


    }
}
