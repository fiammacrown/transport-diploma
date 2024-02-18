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

	[HttpGet("{id}")]
	[Authorize(Roles = "Admin,User")]
	public ActionResult<TransportDto> GetTransport(Guid id)
	{
		var dbTransport = _unitOfWork.TransportRepository.GetById(id);
		if (dbTransport == null)
		{
			return BadRequest();
		}

		return Ok(Mapper.Map(dbTransport));
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

	[HttpGet]
	[Route("GetDeliveryHistory")]
	[Authorize(Roles = "Admin,User")]
	public async Task<ActionResult<TransportHistoryDto>> GetDeliveryHistory(Guid id)
	{
		// загрузим заявки и транспорт в контекст
		await _unitOfWork.LocationRepository.GetAllAsync();
		await _unitOfWork.OrderRepository.GetAllAsync();
		await _unitOfWork.TransportRepository.GetAllAsync();

		var dbDeliveries = _unitOfWork.DeliveryRepository.GetByTransportId(id);

		var transportHistory = new TransportHistoryDto { };
		transportHistory.Records = new List<TransportHistoryRecord> { };

		foreach (var delivery in dbDeliveries)
		{
			var newRecord = new TransportHistoryRecord
			{
				DeliveryId = delivery.Id,
				DeliveredOrderId = delivery.Order.Id,
				DelieveredFrom = delivery.Order.From.Name,
				DelieveredTo = delivery.Order.To.Name,
				DeliveryDate = delivery.EndDate,
			};

			transportHistory.Records.Add(newRecord);

		}

		return Ok(transportHistory);
	}

}
