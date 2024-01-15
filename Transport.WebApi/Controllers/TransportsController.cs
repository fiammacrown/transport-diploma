using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.DAL.Data;
using Transport.DAL.Entities;
using Transport.DTOs;

namespace Transport.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TransportsController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public TransportsController(ApplicationDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<TransportDto>>> GetTransports()
	{

		var dbTransports = await _context.Transports.ToListAsync();

		var trasnports = dbTransports.Select(x => new TransportDto
		{
			Id = x.Id,
			Speed = x.Speed,
			Volume = x.Volume,
			CurrentLoad = x.CurrentLoad,
			AvailableVolume = x.AvailableVolume,
			PricePerKm = x.PricePerKm,
			Status = x.Status.ToString(),
		});

		return Ok(trasnports);
	}
}
