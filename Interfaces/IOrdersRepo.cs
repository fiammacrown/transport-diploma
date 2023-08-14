﻿using Abeslamidze_Kursovaya7.Models;
using System;
using System.Collections.Generic;

namespace Abeslamidze_Kursovaya7.Interfaces
{
    public interface IOrdersRepo
    {
        void Add(Order order);
        List<Order> GetAll();
        Order? GetById(Guid id);
        List<Order> GetByIds(List<Guid> ids);
        List<Order> GetDeliverableOrders();
        List<GroupedOrder> GetDeliverableOrdersGroupByFromTo();
        List<Order> GetInQueue();
        List<Order> GetRegisteredOrders();
    }
}