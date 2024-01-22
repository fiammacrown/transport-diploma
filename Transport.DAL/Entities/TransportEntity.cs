namespace Transport.DAL.Entities;

public enum TransportStatus
{
    InTransit,
    Assigned,
    Free
}

public class TransportEntity
{
    public TransportEntity()
    {
    }

    public TransportEntity(string name, double speed, double volume, double pricePerKm)
    {
        Id = Guid.NewGuid();
        Name = name; 
        Speed = speed;
        Volume = volume;
        PricePerKm = pricePerKm;
        Status = TransportStatus.Free;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Speed { get; set; }
    public double Volume { get; set; }
    public double CurrentLoad { get; set; }
    public double AvailableVolume { get => Volume - CurrentLoad; set { } }
    public double PricePerKm { get; set; }
    public TransportStatus Status { get; set; }


    public void Load(OrderEntity order)
    {
        if ((CurrentLoad + order.Weight) <= Volume)
        {
            CurrentLoad += order.Weight;
        }
        else
        {
            throw new Exception("Invalid operation: Transport.Load");
        };
    }

    public void Unload(OrderEntity order)
    {
        if ((CurrentLoad - order.Weight) >= 0)
        {
            CurrentLoad -= order.Weight;
        }
        else
        {
            throw new Exception("Invalid operation: Transport.Unload");
        };
    }

    public void Free()
    {
        Status = TransportStatus.Free;
    }

    public void Assign()
    {
        Status = TransportStatus.Assigned;
    }

    public void InTransit()
    {
        Status = TransportStatus.InTransit;
    }

    public override string? ToString()
    {
        return Id.ToString();
    }

}
