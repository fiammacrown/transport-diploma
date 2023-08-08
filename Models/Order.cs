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
        public DateTime DateOfIssue { get; }
        public OrderStatus Status { get; }


        public Order(double weight)
        {
            Id = Guid.NewGuid();

            Weight = weight;

            Status = OrderStatus.Unknown;

            DateOfIssue = DateTime.Now;


        }

    }
}
