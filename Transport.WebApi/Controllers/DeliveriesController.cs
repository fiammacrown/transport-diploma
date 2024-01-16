using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.DAL.Data;
using Transport.DAL.Entities;
using Transport.DTOs;

namespace Transport.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DeliveriesController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public DeliveriesController(ApplicationDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<DeliveryDto>>> GetDeliveries()
	{
		var dbDeliveries = await _context.Deliveries.ToListAsync();

		var deliveries = dbDeliveries.Select(x => new DeliveryDto
		{
			Id = x.Id,
			Order = new OrderDto
			{
				Id = x.Order.Id,
				Weight = x.Order.Weight,
				From = x.Order.From.Name,
				To = x.Order.To.Name,
				Status = x.Order.Status.ToString(),
			},
			Transport = new TransportDto
			{
				Id = x.Transport.Id,
				Speed = x.Transport.Speed,
				Volume = x.Transport.Volume,
				CurrentLoad = x.Transport.CurrentLoad,
				AvailableVolume = x.Transport.AvailableVolume,
				PricePerKm = x.Transport.PricePerKm,
				Status = x.Transport.Status.ToString(),
			},
			StartDate = x.StartDate,
			EndDate = x.EndDate,
			Price = x.Price,
			Status = x.Status.ToString(),
		});

		return Ok(deliveries);
	}
}
