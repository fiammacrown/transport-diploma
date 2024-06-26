﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport.DAL.Data;
using Transport.DAL;
using Transport.DTOs;
using Transport.WebApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace Transport.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DeliveriesController : ControllerBase
{
	private readonly UnitOfWork _unitOfWork;
	private readonly DispatchService _dispatchService;

	public DeliveriesController(UnitOfWork unitOfWork, DispatchService dispatchService)
	{
		_unitOfWork = unitOfWork;
		_dispatchService = dispatchService;
	}

	[HttpGet]
	[Authorize(Roles = "Admin,User")]
	public async Task<ActionResult<IEnumerable<DeliveryDto>>> GetDeliveries()
	{
		// загрузим заявки и транспорт в контекст
		await _unitOfWork.LocationRepository.GetAllAsync();
		await _unitOfWork.OrderRepository.GetAllAsync();
		await _unitOfWork.TransportRepository.GetAllAsync();

		var dbDeliveries = await _unitOfWork.DeliveryRepository.GetAllAsync();

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

	[HttpGet]
	[Route("Dispatch")]
	[Authorize(Roles = "Admin,User")]
	public async Task<ActionResult<IEnumerable<DeliveryDto>>> Dispatch()
	{
		try
		{
			var dbDeliveries = await _dispatchService.Dispatch();

			var deliveries = dbDeliveries.Select(Mapper.Map).ToList();

			return Ok(deliveries);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
				"Error dispatching orders");
		}
	}

	[HttpGet]
	[Route("Start")]
	[Authorize(Roles = "Admin,User")]
	public async Task<ActionResult<IEnumerable<DeliveryDto>>> Start()
	{
		try
		{
			var dbDeliveries = await _dispatchService.Start();

			var deliveries = dbDeliveries.Select(Mapper.Map).ToList();

			return Ok(deliveries);
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
				"Error starting delivery");
		}
	}

	[HttpGet("{id}")]
	[Authorize(Roles = "Admin,User")]
	async public Task<ActionResult> UpdateDelivery(Guid id)
	{
		try
		{
			
			await _dispatchService.Update(id);

			return Ok();
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
				"Error updating delivery");
		}
	}

	[HttpGet]
	[Route("UpdateAll")]
	[Authorize(Roles = "Admin,User")]
	async public Task<ActionResult> UpdateAll()
	{
		try
		{

			await _dispatchService.UpdateAll();

			return Ok();
		}
		catch (Exception)
		{
			return StatusCode(StatusCodes.Status500InternalServerError,
				"Error updating deliveries");
		}
	}
}
