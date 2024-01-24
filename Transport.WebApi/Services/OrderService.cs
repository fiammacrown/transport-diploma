using Transport.DAL;
using Transport.DAL.Entities;
using Transport.DTOs;

namespace Transport.WebApi.Services;

public class OrderService
{
	private readonly UnitOfWork _unitOfWork;

	public OrderService(UnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public OrderEntity CreateOrder(NewOrderDto newOrder)
	{
		if (!ValidateOrder(newOrder))
		{
			throw new Exception("Invalid order");
		}

		var fromLocation = _unitOfWork.LocationRepository.GetByName(newOrder.From);
		var toLocation = _unitOfWork.LocationRepository.GetByName(newOrder.To);
		if (fromLocation == null || toLocation == null)
		{
			throw new Exception("Location not found");
		}

		var dbOrder = new OrderEntity {
			Weight = newOrder.Weight,
			FromId = fromLocation.Id,
			ToId = toLocation.Id,
			CreatedDate = DateTime.Now
		};

		_unitOfWork.OrderRepository.Add(dbOrder);
		_unitOfWork.Save();

		return dbOrder;
	}

	public void DeleteOrder(Guid id)
	{
		var dbOrder = _unitOfWork.OrderRepository.GetById(id);
		if (dbOrder == null)
		{
			throw new Exception("Order not found");
		}
		if (dbOrder.Status != OrderStatus.Registered)
		{
			throw new Exception("Order can not be deleted");
		}

		_unitOfWork.OrderRepository.Delete(dbOrder);
		_unitOfWork.Save();
	}
	public OrderEntity UpdateOrder(Guid id, NewOrderDto newOrder)
	{
		if (!ValidateOrder(newOrder))
		{
			throw new Exception("Invalid order");
		}

		var order = _unitOfWork.OrderRepository.GetById(id);
		if (order == null)
		{
			throw new Exception("Order not found");
		}

		var fromLocation = _unitOfWork.LocationRepository.GetByName(newOrder.From);
		var toLocation = _unitOfWork.LocationRepository.GetByName(newOrder.To);
		if (fromLocation == null || toLocation == null)
		{
			throw new Exception("Location not found");
		}

		order.Weight = newOrder.Weight;
		order.From = fromLocation;
		order.To = toLocation;
		order.UpdatedDate =	 DateTime.Now;

		_unitOfWork.OrderRepository.Update(order);
		_unitOfWork.Save();
		return order;

	}

	public bool ValidateOrder(NewOrderDto order)
	{
		if (order.Weight <= 0)
		{
			//throw new Exception("Invalid weight");
			return false;
		}

		double maxVolume = _unitOfWork.TransportRepository.GetMaxVolume();
		if (order.Weight > maxVolume)
		{
			//throw new Exception("Invalid weight");
			return false;
		}
		if (order.From == order.To)
		{
			//throw new Exception("Invalid location");
			return false;
		}

		return true;
	}
}