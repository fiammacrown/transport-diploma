using Transport.DAL.Entities;

namespace Transport.DAL.Interfaces;

public interface IDeliveriesRepo
{ 
    void Add(DeliveryEntity delivery);
    void Update(DeliveryEntity delivery);
    List<DeliveryEntity> GetAll();
    DeliveryEntity? GetById(Guid id);
    List<DeliveryEntity> GetInProgress();
    List<DeliveryEntity> GetNew();
}