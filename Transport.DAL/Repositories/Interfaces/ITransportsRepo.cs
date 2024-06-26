﻿using Transport.DAL.Entities;

namespace Transport.DAL.Interfaces;

public interface ITransportsRepo
{
    void Update(TransportEntity transport);
	Task<List<TransportEntity>> GetAllAsync();
    TransportEntity? GetById(Guid id);
    List<TransportEntity> GetFree();
    List<TransportEntity> GetInTransit();
	List<TransportEntity> GetAssigned();
	double GetPricePerKmById(Guid id);
    double GetSpeedInKmById(Guid id);
    double GetMaxVolume();
}