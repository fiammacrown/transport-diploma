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
        }
        public void Delete(Order order)
        {
            _entityContext.Orders.Remove(order);
        }

        public void Update(Order updated)
        {
            _entityContext.Entry(updated).State = EntityState.Modified;
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

        public List<Order> GetDeliverableOrders()
        {
            var deliverableStatuses = new List<OrderStatus> { OrderStatus.Registered, OrderStatus.InQueue };
            return _entityContext.Orders
                .Where(o => deliverableStatuses.Contains(o.Status))
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
