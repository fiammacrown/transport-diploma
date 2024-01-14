using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.DAL.Data;
using Transport.DAL.Entities;
using Transport.DTOs;

namespace Transport.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	public OrdersController(ApplicationDbContext context)
    {
		_context = context;
	}

    [HttpGet]
	public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
	{
		_context.Locations.Load();

		var dbOrders = await _context.Orders.ToListAsync();

		var orders = dbOrders.Select(x => new OrderDto
		{
			Id = x.Id,
			Weight = x.Weight,
			From = x.From.Name,
			To = x.To.Name,
			Status = x.Status.ToString(),
		});

		return Ok(orders);
	}
}
