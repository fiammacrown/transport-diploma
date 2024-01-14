using Transport.DAL.Entities;

namespace Transport.DAL.Interfaces;

public interface ITransportsRepo
{
    void Update(TransportEntity transport);
    List<TransportEntity> GetAll();
    TransportEntity? GetById(Guid id);
    List<TransportEntity> GetFree();
    List<TransportEntity> GetInTransit();
    double GetPricePerKmById(Guid id);
    double GetSpeedInKmById(Guid id);
    double GetMaxVolume();
}