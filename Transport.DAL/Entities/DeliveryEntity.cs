using System.ComponentModel.DataAnnotations.Schema;

namespace Transport.DAL.Entities;

public enum DeliveryStatus
{
    New,
    InProgress,
    Done
}

public class DeliveryEntity
{
	public DeliveryEntity()
    {
    }

    public DeliveryEntity(Distance distance, Guid orderId, Guid transportId)
    {
        Id = Guid.NewGuid();
        TransportId = transportId;
        OrderId = orderId;
        Distance = distance;

        StartDate = null;
        EndDate = null;
        Price = null;
        Status = DeliveryStatus.New;
    }

    public Guid Id { get; set;  }
    public Guid OrderId { get; set; }
    public OrderEntity Order { get; set; }
   
    public Guid TransportId { get; set; }
    public TransportEntity Transport { get; set; }

    [NotMapped]
    public Distance Distance { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double? Price { get; set; }
    public DeliveryStatus Status { get; set; }

    public void CalculatePrice(double weight, TransportEntity transport)
    {
        Price = (Distance.InKm * transport.PricePerKm) * weight;
    }

    public void InProgress(Distance distance, DateTime date)
    {
        StartDate = date;
        EndDate = StartDate?.AddMinutes(distance.InKm / Transport!.Speed);
        Status = DeliveryStatus.InProgress;
    }

    public void Done()
    {
        Status = DeliveryStatus.Done;
    }

    public override string? ToString()
    {
        return Id.ToString();
    }
}
