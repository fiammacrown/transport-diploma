namespace Transport.DTOs;

public class TransportDto
{
	public Guid Id { get; set; }
	public double Speed { get; set; }
	public double Volume { get; set; }
	public double CurrentLoad { get; set; }
	public double AvailableVolume { get; set; }
	public double PricePerKm { get; set; }
	public string Status { get; set; }
}