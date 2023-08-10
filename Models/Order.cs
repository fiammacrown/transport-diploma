using System;
using System.Collections.Generic;
using System.Linq;

namespace Abeslamidze_Kursovaya7.Models
{
    public enum OrderStatus
    {
        Registered,
        Assigned,
        InProgress,
        InQueue,
        Done,
    }

    public class Order
    {
        public Guid Id { get; }

        public double Weight { get; }
        public Location From { get; }  
        public Location To { get; }
        public DateTime IssueDate { get; }
        public DateTime? DeliveryDate { get; set;}
        public OrderStatus Status { get; set; }


        public Order(double weight, Location from, Location to)
        {
            Id = Guid.NewGuid();

            Weight = weight;

            From = from;    

            To = to;    

            Status = OrderStatus.Registered;

            IssueDate = DateTime.Now;


        }

    }
    public class GroupedOrder
    {
        public GroupedOrder(Location from, Location to, List<Order> orders)
        {
            From = from;
            To = to;
            Orders = orders;
        }

        public Location To { get; }
        public Location From { get;  }
        public int Count { get => Orders.Count; }
        public double TotalWeight { get => Orders.Sum(o => o.Weight); }
        public List<Order> Orders { get; }

    }
}
