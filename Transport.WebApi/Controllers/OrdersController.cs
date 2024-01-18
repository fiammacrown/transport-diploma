using Microsoft.AspNetCore.Mvc;
using Transport.DAL;
using Transport.DAL.Entities;
using Transport.DTOs;
using Transport.WebApi.Services;

namespace Transport.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
	private readonly UnitOfWork _unitOfWork;
	private readonly OrderService _orderService;

	public OrdersController(UnitOfWork unitOfWork, OrderService orderService)
    {
		_unitOfWork = unitOfWork;
		_orderService = orderService;
	}

	[HttpPost]
	public ActionResult<OrderDto> CreateOrder([FromBody] OrderDto order)
	{
		try
		{
			if (order == null)
				return BadRequest();

			var createdOrder = _orderService.CreateOrder(order);

			return CreatedAtAction(nameof(GetOrder),
				new { id = createdOrder.Id }, createdOrder);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
				"Error creating new order");
		}
	}
	[HttpGet("{id}")]
	public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
	{
		await _unitOfWork.LocationRepository.GetAllAsync();

		try
		{
			var dbOrder = _unitOfWork.OrderRepository.GetById(id);

			if (dbOrder == null)
			{
				return NotFound();
			}

			var order = Mapper.Map(dbOrder);
			return Ok(order);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
				"Error retrieving data from the database");
		}
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
	{
		await _unitOfWork.LocationRepository.GetAllAsync();

		var dbOrders = await _unitOfWork.OrderRepository.GetAllAsync();

		var orders = dbOrders.Select(Mapper.Map);

		return Ok(orders);
	}

	[HttpPut("{id}")]
	public ActionResult<OrderDto> UpdateOrder(Guid id, [FromBody] OrderDto orderDto)
	{
		var dbOrder = _orderService.UpdateOrder(id, orderDto);

		var order = Mapper.Map(dbOrder);

		return Ok(order);
	}

	[HttpDelete("{id}")]
	public ActionResult DeleteOrder(Guid id)
	{
		try
		{
			_orderService.DeleteOrder(id);
			return Ok();
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
				"Error deleting data");
		}
	}

}

public static class Mapper
{
	public static OrderDto Map(OrderEntity x)
	{
		return new OrderDto
		{
			Id = x.Id,
			Weight = x.Weight,
			From = Map(x.From),
			To = Map(x.To),
			Status = x.Status.ToString(),
		};
	}

	public static string Map(LocationEntity x)
	{
		return x.Name;
	}
}
