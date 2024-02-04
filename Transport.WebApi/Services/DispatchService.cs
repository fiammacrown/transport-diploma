using System.Linq;
using Transport.DAL;
using Transport.DAL.Entities;

namespace Transport.WebApi.Services
{
	public class DispatchService
	{
		private readonly UnitOfWork _unitOfWork;

		public DispatchService(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		async public Task<List<DeliveryEntity>> Dispatch()
		{
			var newDeliveries = new List<DeliveryEntity>();

			// загрузим локации в контекст
			await _unitOfWork.LocationRepository.GetAllAsync();

			var deliverableOrders = _unitOfWork.OrderRepository.GetDeliverableOrders();
			var freeTransport = _unitOfWork.TransportRepository.GetFree();

			if (deliverableOrders.Count == 0 || freeTransport.Count == 0)
			{
				return newDeliveries;
			}

			var _temp = new Dictionary<Distance, TransportEntity>();
			foreach (var order in deliverableOrders)
			{
				var distance = new Distance(order.From, order.To);

				// проверяем уже распределенный транспорт по тому же маршруту
				if (_temp.TryGetValue(distance, out var tr))
				{
					if (order.Weight <= tr.AvailableVolume)
					{
						var newDelivery = new DeliveryEntity(
						 distance,
						 order.Id,
						 tr.Id
						);

						newDelivery.CalculatePrice(order.Weight, tr);
						_unitOfWork.DeliveryRepository.Add(newDelivery);
						newDeliveries.Add(newDelivery);

						tr.Load(order);
						_unitOfWork.TransportRepository.Update(tr);

						order.Assign();
						_unitOfWork.OrderRepository.Update(order);

						continue;
					}
				}

		
				foreach (var transport in freeTransport)
				{
					if (transport.Status == TransportStatus.Assigned)
					{
						continue;
					}

					if (order.Weight <= transport.AvailableVolume)
					{

						var newDelivery = new DeliveryEntity(
							distance,
							order.Id,
							transport.Id
						);

						newDelivery.CalculatePrice(order.Weight, transport);
						_unitOfWork.DeliveryRepository.Add(newDelivery);
						newDeliveries.Add(newDelivery);

						transport.Assign();
						transport.Load(order);
						_unitOfWork.TransportRepository.Update(transport);

						order.Assign();
						_unitOfWork.OrderRepository.Update(order);

						_temp.Add(distance, transport);

						break;
					}

				}

			}

			foreach (var order in deliverableOrders)
			{
				if (order.Status != OrderStatus.Assigned)
				{
					order.InQueue();

					_unitOfWork.OrderRepository.Update(order);
				}
			}

			_unitOfWork.Save();
			return newDeliveries;
		}
		async public Task<List<DeliveryEntity>> Start()
		{
			var inProgressDeliveries = new List<DeliveryEntity>();

			var newDeliveries = _unitOfWork.DeliveryRepository.GetNew();
			if (newDeliveries.Count == 0)
			{
				return inProgressDeliveries;
			}
			// загрузим заявки и транспорт в контекст
			await _unitOfWork.LocationRepository.GetAllAsync();
			await _unitOfWork.OrderRepository.GetAllAsync();
			await _unitOfWork.TransportRepository.GetAllAsync();

			DateTime start = DateTime.Now;

			foreach (var delivery in newDeliveries)
			{
				var distance = new Distance(delivery.Order.From, delivery.Order.To);

				delivery.InProgress(distance, start);
				delivery.Order.InProgress();
				delivery.Transport.InTransit();

				_unitOfWork.DeliveryRepository.Update(delivery);
				_unitOfWork.OrderRepository.Update(delivery.Order);
				_unitOfWork.TransportRepository.Update(delivery.Transport);

				inProgressDeliveries.Add(delivery);
			}

			_unitOfWork.Save();
			return inProgressDeliveries;
		}

		async public Task Update(Guid id)
		{
			var inProgressDelivery = _unitOfWork.DeliveryRepository.GetById(id);

			if(inProgressDelivery == null)
			{
				throw new Exception("Delivery not found");
			}
			if (inProgressDelivery.Status != DeliveryStatus.InProgress)
			{
				throw new Exception("Delivery not InProgress");
			}

			if (inProgressDelivery.EndDate > DateTime.Now)
			{
				throw new Exception("Delivery not finished yet");
			}

			// загрузим заявки и транспорт в контекст
			await _unitOfWork.LocationRepository.GetAllAsync();
			await _unitOfWork.OrderRepository.GetAllAsync();
			await _unitOfWork.TransportRepository.GetAllAsync();

			inProgressDelivery.Done();
			inProgressDelivery.Order.Done(inProgressDelivery.EndDate);
			inProgressDelivery.Transport.Unload(inProgressDelivery.Order);
			inProgressDelivery.Transport.Free();

			_unitOfWork.DeliveryRepository.Update(inProgressDelivery);
			_unitOfWork.OrderRepository.Update(inProgressDelivery.Order);
			_unitOfWork.TransportRepository.Update(inProgressDelivery.Transport);
			
			_unitOfWork.Save();
		}

		async public Task UpdateAll()
		{
			var deliveries = _unitOfWork.DeliveryRepository.GetInProgress();

			if (deliveries.Count <= 0)
			{
				return;
			}

			// загрузим заявки и транспорт в контекст
			await _unitOfWork.LocationRepository.GetAllAsync();
			await _unitOfWork.OrderRepository.GetAllAsync();
			await _unitOfWork.TransportRepository.GetAllAsync();

			foreach (var inProgressDelivery in deliveries)
			{
				if (inProgressDelivery.EndDate <= DateTime.Now)
				{
					inProgressDelivery.Done();
					inProgressDelivery.Order.Done(inProgressDelivery.EndDate);
					inProgressDelivery.Transport.Unload(inProgressDelivery.Order);
					inProgressDelivery.Transport.Free();

					_unitOfWork.DeliveryRepository.Update(inProgressDelivery);
					_unitOfWork.OrderRepository.Update(inProgressDelivery.Order);
					_unitOfWork.TransportRepository.Update(inProgressDelivery.Transport);

					_unitOfWork.Save();
				}

			}
		}
	}
}
