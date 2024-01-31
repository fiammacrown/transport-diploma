using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transport.DAL;
using Transport.DTOs;

namespace Transport.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
	private readonly UnitOfWork _unitOfWork;

	public LocationsController(UnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	[HttpGet]
	[Authorize(Roles = "Admin,User")]
	public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations()
	{
		var dbLocations = await _unitOfWork.LocationRepository.GetAllAsync();

		var locations = dbLocations.Select(Mapper.Map).ToList();

		return Ok(locations);
	}
}
