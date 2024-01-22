using Transport.DAL.Entities;
using Transport.DTOs;

namespace Transport.WebApi;

public static class Mapper
{
    public static OrderDto Map(OrderEntity x)
    {
        return new OrderDto
        {
            Id = x.Id,
            Weight = x.Weight,
            From = MapNameOnly(x.From),
            To = MapNameOnly(x.To),
            Status = x.Status.ToString(),
        };
    }

    public static string MapNameOnly(LocationEntity x)
    {
        return x.Name;
    }

    public static LocationDto Map(LocationEntity x)
    {
        return new LocationDto
        {
            Id = x.Id,
            Name = x.Name,
        };
	}

    public static DeliveryDto Map(DeliveryEntity x)
    {
        return new DeliveryDto
        {
            Id = x.Id,
            Order = Map(x.Order),
            Transport = Map(x.Transport),
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            Price = x.Price,
            Status = x.Status.ToString(),

		};
    }
    public static TransportDto Map(TransportEntity x)
    {
        return new TransportDto
        {
            Id = x.Id,
            Name = x.Name,
            Speed = x.Speed,
            Volume = x.Volume,
            CurrentLoad = x.CurrentLoad,
            AvailableVolume = x.AvailableVolume,
            PricePerKm = x.PricePerKm,
            Status = x.Status.ToString(),


        };
    }
}
