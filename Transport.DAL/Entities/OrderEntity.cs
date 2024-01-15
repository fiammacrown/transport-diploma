using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transport.DAL.Entities;

public enum OrderStatus
{
    Registered,
    Assigned,
    InProgress,
    InQueue,
    Done,
}

public class OrderEntity
{
	public OrderEntity()
	{
	}

	public OrderEntity(double weight, Guid fromId, Guid toId)
    {
        Id = Guid.NewGuid();

        Weight = weight;

        FromId = fromId;

        ToId = toId; 

        Status = OrderStatus.Registered;
    }

	public Guid Id { get; set; }
	public double Weight { get; set; }
	public Guid FromId { get; set; }
	public LocationEntity From { get; set; }
	public Guid ToId { get; set; }
	public LocationEntity To { get; set; }
	public OrderStatus Status { get; set; }

	public void Assign()
    {
        Status = OrderStatus.Assigned;
    }

    public void InQueue()
    {
        Status = OrderStatus.InQueue;
    }

    public void InProgress()
    {
        Status = OrderStatus.InProgress;
    }

    public void Done()
    {
        Status = OrderStatus.Done;
    }

    public override string? ToString()
    {
        return Id.ToString();
    }
}
