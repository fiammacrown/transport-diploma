using Microsoft.EntityFrameworkCore;
using Transport.DAL.Data;
using Transport.DAL.Entities;
using Transport.DAL.Interfaces;

namespace Transport.DAL.Repositories;

public class DatabaseOrdersRepo : IOrdersRepo
{
    private readonly ApplicationDbContext _entityContext;

    public DatabaseOrdersRepo(ApplicationDbContext entityContext)
    {
        _entityContext = entityContext;
    }

    public void Add(OrderEntity order)
    {
        _entityContext.Orders.Add(order);
    }
    public void Delete(OrderEntity order)
    {
        _entityContext.Orders.Remove(order);
    }

    public void Update(OrderEntity updated)
    {
        _entityContext.Entry(updated).State = EntityState.Modified;
    }

    public Task<List<OrderEntity>> GetAllAsync()
    {
        //_entityContext.Locations.Load();
        return _entityContext.Orders.ToListAsync();
    }

    public OrderEntity? GetById(Guid id)
    {
        return _entityContext.Orders
            .FirstOrDefault(o => o.Id == id);
    }
    public List<OrderEntity> GetByIds(List<Guid> ids)
    {
        return _entityContext.Orders
            .Where(o => ids.Contains(o.Id))
            .ToList();
    }

    public List<OrderEntity> GetDeliverableOrders()
    {
        var deliverableStatuses = new List<OrderStatus> { OrderStatus.Registered, OrderStatus.InQueue };
        return _entityContext.Orders
            .Where(o => deliverableStatuses.Contains(o.Status))
            .OrderByDescending(o => o.Weight)
            .ToList();
    }
    public  List<OrderEntity> GetInQueue()
    {
        return _entityContext.Orders
            .Where(o => o.Status == OrderStatus.InQueue)
            .ToList();
    }
    public List<OrderEntity> GetRegisteredOrders()
    {
        return _entityContext.Orders
            .Where(o => o.Status == OrderStatus.Registered)
            .OrderByDescending(o => o.Weight)
            .ToList();
    }
}
