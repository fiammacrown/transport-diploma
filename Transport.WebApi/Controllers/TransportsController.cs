using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.DAL;
using Transport.DAL.Data;
using Transport.DTOs;

namespace Transport.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TransportsController : ControllerBase
{
	private readonly ApplicationDbContext _context;
	private readonly UnitOfWork _unitOfWork;


	public TransportsController(ApplicationDbContext context, UnitOfWork unitOfWork)
	{
		_context = context;
		_unitOfWork = unitOfWork;
	}

	[HttpGet]
	[Authorize(Roles = "Admin,User")]
	public async Task<ActionResult<IEnumerable<TransportDto>>> GetTransports()
	{

		var dbTransports = await _context.Transports.ToListAsync();

		var transports = dbTransports.Select(Mapper.Map).ToList();

		return Ok(transports);
	}


	[HttpGet]
	[Route("GetMaxVolume")]
	[Authorize(Roles = "Admin,User")]
	public ActionResult<double> GetMaxVolume()
	{
		var maxVolume = _unitOfWork.TransportRepository.GetMaxVolume();

		return Ok(maxVolume);
	}

}
