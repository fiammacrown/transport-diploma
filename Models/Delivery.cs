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
        InProgress,
        Done
    }
    public class Delivery
    {
        public Delivery()
        {
        }

        public Delivery(Location from, Location to,  Transport transport)
        {
            Id = Guid.NewGuid();
            From = from;
            To = to;
            Transport = transport;
            StartDate = DateTime.Now;
            Status = DeliveryStatus.InProgress;
        }

        public Guid Id { get; set;  }
        public Transport Transport { get; set; }
        public Location From { get; set; }
        public Location To { get; set; }    
        public double TotalPrice { get; set; }
        public double TotalWeight { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DeliveryStatus Status { get; set; }

    }
}
