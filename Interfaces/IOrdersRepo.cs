using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;

namespace Abeslamidze_Kursovaya7.Interfaces
{
    public interface IOrdersRepo
    {
        void Add(Order order);
        void Update(Order order);
        List<Order> GetAll();
        Order? GetById(Guid id);
        List<Order> GetByIds(List<Guid> ids);
        List<Order> GetDeliverableOrders();
        List<Order> GetRegisteredOrders();
        List<Order> GetInQueue();

    }
}