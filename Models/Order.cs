using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abeslamidze_Kursovaya7.Models
{
    public enum OrderStatus
    {
        Registered,
        InProgress,
        InQueue,
        Unknown,
        Done,
    }

    public class Order
    {
        public Guid Id { get; }

        public double Weight { get; }
        public Location From { get; }  
        public Location To { get; }
        public DateTime DateOfIssue { get; }
        public OrderStatus Status { get; }


        public Order(double weight, Location from, Location to)
        {
            Id = Guid.NewGuid();

            Weight = weight;

            From = from;    

            To = to;    

            Status = OrderStatus.Registered;

            DateOfIssue = DateTime.Now;


        }

    }
}
