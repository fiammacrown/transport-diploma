using System;
using System.Collections.Generic;
using System.Linq;
using Abeslamidze_Kursovaya7.Models;

namespace Abeslamidze_Kursovaya7.Repos
{
    public class OrdersRepo
    {
        private List<Order> _orders = new List<Order>
        {
            new Order(1500, new Location("Брест"), new Location("Минск")),
            new Order(1000, new Location("Брест"), new Location("Минск")),
            new Order(1100, new Location("Минск"), new Location("Брест")),
        };

        public void Add(Order order)
        {
            _orders.Add(order);
        }

        public List<Order> GetAll()
        {
            return _orders;
        }

        public Order? GetById(Guid id)
        {
            return _orders
                .FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetByIds(List<Guid> ids)
        {
            var orders = new List<Order>();
            foreach (var i in ids)
            {
                var order = GetById(i);
                if (order != null)
                {
                    orders.Add(order);
                }
               
            }
            return orders;
        }

        public List<GroupedOrder> GetDeliverableOrdersGroupByFromTo()
        {
            return GetDeliverableOrders()
               .GroupBy(order => new Distance(order.From, order.To))
               .Select(groupedOrder => new GroupedOrder(
                   groupedOrder.Key.From,
                   groupedOrder.Key.To,
                   groupedOrder.ToList()
                   )
               )
               .OrderByDescending(o => o.TotalWeight)
               .ToList();
        }
        public List<Order> GetDeliverableOrders()
        {
            var deliverableStatuses = new List<OrderStatus> { OrderStatus.Registered, OrderStatus.InQueue };
            return _orders
                .Where(o => deliverableStatuses.Contains(o.Status) )
                .OrderByDescending(o => o.Weight)
                .ToList();
        }

        public List<Order> GetRegisteredOrders()
        {
            return _orders
                .Where(o => o.Status == OrderStatus.Registered)
                .OrderByDescending(o => o.Weight)
                .ToList();
        }

        public List<Order> GetInQueue()
        {
            return _orders
                .Where(o => o.Status == OrderStatus.InQueue)
                .ToList();
        }


    }
}
