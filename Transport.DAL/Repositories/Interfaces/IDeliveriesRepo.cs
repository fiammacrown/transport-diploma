using Transport.DAL.Entities;

namespace Transport.DAL.Interfaces;

public interface IDeliveriesRepo
{ 
    void Add(DeliveryEntity delivery);
    void Update(DeliveryEntity delivery);
    Task<List<DeliveryEntity>> GetAllAsync();
    DeliveryEntity? GetById(Guid id);
    List<DeliveryEntity> GetInProgress();
    List<DeliveryEntity> GetNew();
	List<DeliveryEntity> GetByTransportId(Guid transportId);
}