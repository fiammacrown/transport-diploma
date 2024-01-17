using Transport.DAL.Entities;

namespace Transport.DAL.Interfaces;

public interface ILocationsRepo
{
    Task<List<LocationEntity>> GetAllAsync();

    LocationEntity? GetByName(string name);
    LocationEntity? GetById(Guid id);

}
