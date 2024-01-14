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

    public List<LocationEntity> GetAll()
    {
        //_entityContext.Locations.Load();
        return _entityContext.Locations.Local.ToList();
    }

    public LocationEntity? GetById(Guid id)
    {
        return _entityContext.Locations
            .FirstOrDefault(t => t.Id == id);
    }
}
