using Microsoft.EntityFrameworkCore;
using Transport.DAL.Data;
using Transport.DAL.Entities;
using Transport.DAL.Interfaces;

namespace Transport.DAL.Repositories;

public class DatabaseTransportsRepo : ITransportsRepo
{
    private readonly ApplicationDbContext _entityContext;

    public DatabaseTransportsRepo(ApplicationDbContext entityContext)
    {
        _entityContext = entityContext;
    }

    public void Update(TransportEntity updated)
    {
        _entityContext.Entry(updated).State = EntityState.Modified;
    }

	public Task<List<TransportEntity>> GetAllAsync()
	{
        //_entityContext.Transports.Load();
        return _entityContext.Transports.ToListAsync();
	}

    public TransportEntity? GetById(Guid id)
    {
        return _entityContext.Transports
            .FirstOrDefault(t => t.Id == id);
    }


    public List<TransportEntity> GetFree()
    {
        return _entityContext.Transports
            .Where(t => t.Status == TransportStatus.Free)
            .OrderByDescending(t => t.Volume)
            .ToList(); ;
    }

    public List<TransportEntity> GetInTransit()
    {
        return _entityContext.Transports
            .Where(t => t.Status == TransportStatus.InTransit)
            .OrderByDescending(t => t.Volume)
            .ToList(); ;
    }

	public List<TransportEntity> GetAssigned()
	{
		return _entityContext.Transports
			.Where(t => t.Status == TransportStatus.Assigned)
			.OrderByDescending(t => t.Volume)
			.ToList(); ;
	}

	public double GetSpeedInKmById(Guid id)
    {
        return _entityContext.Transports
            .Where(t => t.Id == id)
            .Select(t => t.Speed)
            .First();
    }

    public double GetPricePerKmById(Guid id)
    {
        return _entityContext.Transports
            .Where(t => t.Id == id)
            .Select(t => t.PricePerKm)
            .First();
    }

    public double GetMaxVolume()
    {
        return _entityContext.Transports
            .Select(t => t.Volume)
            .Max();
    }
}
