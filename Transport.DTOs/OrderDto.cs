namespace Transport.DTOs;

public class OrderDto
{
	public Guid Id { get; set; }

	public double Weight { get; set; }

	public string From { get; set; }

	public string To { get; set; }

	public DateTime? CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }
	public DateTime? DeliveredDate { get; set; }

	public string Status { get; set; }
}
