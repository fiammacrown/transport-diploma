﻿using Transport.DAL.Entities;

namespace Transport.DAL.Interfaces;

public interface IOrdersRepo
{
    void Add(OrderEntity order);
    void Update(OrderEntity order);
    void Delete(OrderEntity order);
    Task<List<OrderEntity>> GetAllAsync();
    OrderEntity? GetById(Guid id);
    List<OrderEntity> GetByIds(List<Guid> ids);
    List<OrderEntity> GetDeliverableOrders();
    List<OrderEntity> GetRegisteredOrders();
    List<OrderEntity> GetInQueue();
	List<OrderEntity> GetAssigned();

}