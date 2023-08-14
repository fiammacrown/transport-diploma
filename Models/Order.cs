using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Guid Id { get; set; }

        public double Weight { get; set; }

        public Location From { get; set; }
        public Location To { get; set; }
        public DateTime IssueDate { get; set; }
        public double? DeliveryPrice { get; set; }
        public DateTime? DeliveryDate { get; set;}
        public OrderStatus Status { get; set; }


        public Order()
        {
        }

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

        public Location To { get; set; }
        public Location From { get; set; }
        public int Count { get => Orders.Count; }
        public double Weight { get => Orders.Sum(o => o.Weight); }
        public List<Order> Orders { get; }

    }
}
