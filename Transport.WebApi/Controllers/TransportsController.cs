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
	public async Task<ActionResult<IEnumerable<TransportDto>>> GetTransports()
	{

		var dbTransports = await _context.Transports.ToListAsync();

		var transports = dbTransports.Select(x => new TransportDto
		{
			Id = x.Id,
			Speed = x.Speed,
			Volume = x.Volume,
			CurrentLoad = x.CurrentLoad,
			AvailableVolume = x.AvailableVolume,
			PricePerKm = x.PricePerKm,
			Status = x.Status.ToString(),
		});

		return Ok(transports);
	}


	[HttpGet]
	[Route("GetMaxVolume")]
	public ActionResult<double> GetMaxVolume()
	{
		var maxVolume = _unitOfWork.TransportRepository.GetMaxVolume();

		return Ok(maxVolume);
	}

}
