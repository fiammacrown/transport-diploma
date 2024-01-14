using Transport.DAL.Entities;

namespace Transport.DAL.Interfaces;

public interface ILocationsRepo
{
    List<LocationEntity> GetAll();
    LocationEntity? GetById(Guid id);

}
