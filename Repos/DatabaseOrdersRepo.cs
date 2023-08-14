using Abeslamidze_Kursovaya7.Interfaces;
using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


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
        public List<Order> GetAll()
        {
            _entityContext.Orders.Load();
            return _entityContext.Orders.Local.ToList();
        }
        public Order? GetById(Guid id)
        {
            return null;
        }
        public List<Order> GetByIds(List<Guid> ids)
        {
            return new List<Order>();
        }
        public List<Order> GetDeliverableOrders()
        {
            // TODO:
            return new List<Order>();
        }
        public List<GroupedOrder> GetDeliverableOrdersGroupByFromTo()
        {
            // TODO:
            return new List<GroupedOrder>();
        }
        public  List<Order> GetInQueue()
        {
            // TODO:
            return new List<Order>();
        }
        public List<Order> GetRegisteredOrders()
        {
            // TODO:
            return new List<Order>();
        }
    }
}
