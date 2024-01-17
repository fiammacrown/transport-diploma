using Microsoft.EntityFrameworkCore;
using Transport.DAL.Data;
using Transport.DAL.Entities;
using Transport.DAL.Interfaces;

namespace Transport.DAL.Repositories;

public class DatabaseLocationRepo : ILocationsRepo
{
    private readonly ApplicationDbContext _entityContext;

    public DatabaseLocationRepo(ApplicationDbContext entityContext)
    {
        _entityContext = entityContext;
    }

    public Task<List<LocationEntity>> GetAllAsync()
    {
        return _entityContext.Locations.ToListAsync();
    }


    public LocationEntity? GetById(Guid id)
    {
        return _entityContext.Locations
            .FirstOrDefault(t => t.Id == id);
    }

	public LocationEntity? GetByName(string name)
	{
		return _entityContext.Locations
			.FirstOrDefault(t => t.Name == name);
	}
}
