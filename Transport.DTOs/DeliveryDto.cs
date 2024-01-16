using System.ComponentModel.DataAnnotations.Schema;

namespace Transport.DTOs;

public class DeliveryDto
{
	public Guid Id { get; set; }
	public OrderDto Order { get; set; }
	public TransportDto Transport { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public double? Price { get; set; }
	public string Status { get; set; }
}
