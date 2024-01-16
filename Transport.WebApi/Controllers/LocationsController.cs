using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.DAL.Data;
using Transport.DAL.Entities;
using Transport.DTOs;

namespace Transport.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public LocationsController(ApplicationDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations()
	{

		var dbLocations = await _context.Locations.ToListAsync();

		var locations = dbLocations.Select(x => new LocationDto
		{
			Id = x.Id,
			Name = x.Name,
		});

		return Ok(locations);
	}
}
