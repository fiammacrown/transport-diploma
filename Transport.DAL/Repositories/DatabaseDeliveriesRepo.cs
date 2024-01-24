using Microsoft.EntityFrameworkCore;
using Transport.DAL.Data;
using Transport.DAL.Entities;
using Transport.DAL.Interfaces;

namespace Transport.DAL.Repositories;

public class DatabaseDeliveriesRepo : IDeliveriesRepo
{
    private readonly ApplicationDbContext _entityContext;

    public DatabaseDeliveriesRepo(ApplicationDbContext entityContext)
    {
        _entityContext = entityContext;
    }

    public void Add(DeliveryEntity delivery)
    {
        _entityContext.Deliveries.Add(delivery);
    }

    public void Update(DeliveryEntity updated)
    {
        _entityContext.Entry(updated).State = EntityState.Modified;
    }

    public Task<List<DeliveryEntity>> GetAllAsync()
    {
        //_entityContext.Deliveries.Load();
        return _entityContext.Deliveries.
            OrderBy(d => d.Status).ToListAsync();
	}

    public DeliveryEntity? GetById(Guid id)
    {
        return _entityContext.Deliveries
            .FirstOrDefault(d => d.Id == id); ;
    }

    public List<DeliveryEntity> GetInProgress()
    {
        return _entityContext.Deliveries
            .Where(d => d.Status == DeliveryStatus.InProgress)
            .ToList();
    }

    public List<DeliveryEntity> GetNew()
    {
        return _entityContext.Deliveries
            .Where(d => d.Status == DeliveryStatus.New)
            .ToList();
    }
}
