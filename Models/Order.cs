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
        public Guid FromId { get; set; }
        [ForeignKey("FromId")]
        public Location From { get; set; }
        public Guid ToId { get; set; }
        [ForeignKey("ToId")]
        public Location To { get; set; }
        public OrderStatus Status { get; set; }


        public Order()
        {
        }

        public Order(double weight, Guid fromId, Guid toId)
        {
            Id = Guid.NewGuid();

            Weight = weight;

            FromId = fromId;

            ToId = toId; 

            Status = OrderStatus.Registered;
        }

        public void Assign()
        {
            Status = OrderStatus.Assigned;
        }

        public void InQueue()
        {
            Status = OrderStatus.InQueue;
        }

        public void InProgress()
        {
            Status = OrderStatus.InProgress;
        }

        public void Done()
        {
            Status = OrderStatus.Done;
        }

        public override string? ToString()
        {
            return Id.ToString();
        }
    }
}
