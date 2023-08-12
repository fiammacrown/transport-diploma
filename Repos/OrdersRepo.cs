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
            new Order(50, new Location("Брест"), new Location("Минск")),
            new Order(100, new Location("Брест"), new Location("Минск")),
            new Order(100, new Location("Минск"), new Location("Брест")),
        };


        public List<Order> GetAll()
        {
            return _orders;
        }

        public List<GroupedOrder> GetRegisteredOrdersGroupByFromTo()
        {
            return GetRegisteredOrders()
               .GroupBy(order => new Distance(order.From, order.To))
               .Select(groupedOrder => new GroupedOrder(
                   groupedOrder.Key.From,
                   groupedOrder.Key.To,
                   groupedOrder.ToList()
                   )
               )
               .OrderByDescending(o => o.TotalWeight).ToList()
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

        public Order? GetById(Guid id)
        {
            return _orders
                .FirstOrDefault(o => o.Id == id);
        }

    }
}
