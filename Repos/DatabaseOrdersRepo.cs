using Abeslamidze_Kursovaya7.Interfaces;
using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;


namespace Abeslamidze_Kursovaya7.Repos
{
    public class DatabaseOrdersRepo : IOrdersRepo
    {
        private readonly EntityContext _entityContext;

        public DatabaseOrdersRepo(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        public void Add(Order order)
        {
            _entityContext.Orders.Add(order);
            _entityContext.SaveChanges();
        }

        public void Update(Order updated)
        {
            var existing = GetById(updated.Id);
            if (existing != null)
            {
                _entityContext.Entry(existing).CurrentValues.SetValues(updated);
                _entityContext.SaveChanges();
            }
        }
        public List<Order> GetAll()
        {
            _entityContext.Orders.Load();
            return _entityContext.Orders.Local.ToList();
        }
        public Order? GetById(Guid id)
        {
            return _entityContext.Orders
                .FirstOrDefault(o => o.Id == id);
        }
        public List<Order> GetByIds(List<Guid> ids)
        {
            return _entityContext.Orders
                .Where(o => ids.Contains(o.Id))
                .ToList();
        }

        public List<Order> GetByTransportId(Guid transportId)
        {
            return _entityContext.Orders
                .Where(o => o.Transport.Id == transportId)
                .ToList();
        }
        public List<Order> GetDeliverableOrders()
        {
            var deliverableStatuses = new List<OrderStatus> { OrderStatus.Registered, OrderStatus.InQueue };
            return _entityContext.Orders
                .Where(o => deliverableStatuses.Contains(o.Status))
                .OrderByDescending(o => o.Weight)
                .ToList();
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
                .OrderByDescending(o => o.Weight)
                .ToList();
        }
        public  List<Order> GetInQueue()
        {
            return _entityContext.Orders
                .Where(o => o.Status == OrderStatus.InQueue)
                .ToList();
        }
        public List<Order> GetRegisteredOrders()
        {
            return _entityContext.Orders
                .Where(o => o.Status == OrderStatus.Registered)
                .OrderByDescending(o => o.Weight)
                .ToList();
        }
    }
}
