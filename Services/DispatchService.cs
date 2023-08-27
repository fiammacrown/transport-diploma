using System.Collections.Generic;
using Abeslamidze_Kursovaya7.Models;
using System;

namespace Abeslamidze_Kursovaya7.Services
{
    public class DispatchService
    {
        public UnitOfWork unitOfWork;

        private Dictionary<Distance, Transport>  _temp = new Dictionary<Distance, Transport>();

        public DispatchService(UnitOfWork u)
        {
            unitOfWork = u;

        }

        public void Dispatch()
        {
            DispatchOrders();
            unitOfWork.Save();

        }

        public void Start()
        {
            StartDeliveries();
            unitOfWork.Save();

        }

        public void Update()
        {
            UpdateDeliveries();
            unitOfWork.Save();
        }

        public void UpdateDeliveries()
        {
            var inProgressDeliveries = unitOfWork.DeliveryRepository.GetInProgress();
            foreach (var delivery in inProgressDeliveries)
            {
                if (delivery.EndDate <= DateTime.Now)
                {
                    delivery.Done();
                    delivery.Order.Done();
                    delivery.Transport.Unload(delivery.Order);
                    delivery.Transport.Free();

                    unitOfWork.DeliveryRepository.Update(delivery);
                    unitOfWork.OrderRepository.Update(delivery.Order);
                    unitOfWork.TransportRepository.Update(delivery.Transport);


                }
            }

        }

        private void DispatchOrders()
        {
            var deliverableOrders = unitOfWork.OrderRepository.GetDeliverableOrders();
            var freeTransport = unitOfWork.TransportRepository.GetFree();

            foreach (var order in deliverableOrders)
            {
                var distance = new Distance(order.From, order.To);

                // проверяем уже распределенный транспорт по тому же маршруту
                if (_temp.TryGetValue(distance, out var value))
                {
                    if (order.Weight <= value.AvailableVolume)
                    {
                        var newDelivery = new Delivery(
                         distance,
                         order.Id,
                         value.Id
                        );

                        unitOfWork.DeliveryRepository.Add(newDelivery);

                        value.Load(order);
                        unitOfWork.TransportRepository.Update(value);

                        order.Assign();
                        unitOfWork.OrderRepository.Update(order);

                        break;
                    }
                }

                else
                {
                    foreach (var transport in freeTransport)
                    {   
                        if (transport.Status == TransportStatus.Assigned)
                        {
                            continue;
                        }

                        if (order.Weight <= transport.AvailableVolume)
                        {

                            var newDelivery = new Delivery(
                                distance,
                                order.Id,
                                transport.Id
                            );

                            unitOfWork.DeliveryRepository.Add(newDelivery);

                            transport.Assign();
                            transport.Load(order);
                            unitOfWork.TransportRepository.Update(transport);

                            order.Assign();
                            unitOfWork.OrderRepository.Update(order);

                            _temp.Add(distance, transport);

                            break;
                        }

                    }

                }

            }

            foreach (var order in deliverableOrders)
            {
                if (order.Status != OrderStatus.Assigned)
                {
                    order.InQueue();
                    unitOfWork.OrderRepository.Update(order);
                }
            }
        }      
        
        private void StartDeliveries()
        {
            DateTime start = DateTime.Now;

            var newDeliveries = unitOfWork.DeliveryRepository.GetNew();

            foreach (var delivery in newDeliveries)
            {
                delivery.InProgress(start);
                delivery.Order.InProgress();
                delivery.Transport.InTransit();

                unitOfWork.DeliveryRepository.Update(delivery);
                unitOfWork.OrderRepository.Update(delivery.Order);
                unitOfWork.TransportRepository.Update(delivery.Transport);
            }
        }
    }

    public class DispatchServiceResult
    {
        public DispatchServiceResult(List<Delivery> inProgressDeliveries, List<Order> inQueueOrders, List<Order> deliverableOrders, List<Transport> freeTransport)
        {
            InProgressDeliveries = inProgressDeliveries;
            DeliverableOrders = deliverableOrders;
            InQueueOrders = inQueueOrders;
            FreeTransport = freeTransport;
        }

        private List<Delivery> InProgressDeliveries { get; }
        private List<Order> DeliverableOrders { get; }
        private List<Order> InQueueOrders { get; }
        private List<Transport> FreeTransport { get; }

        public int NumOfInProgressDeliveries { get => InProgressDeliveries.Count; }

        public int NumOfDeliverableOrders { get => DeliverableOrders.Count; }
        public int NumOfInQueueOrders { get => InQueueOrders.Count; }
        public int NumOfFreeTransport { get => FreeTransport.Count; }


    }
}
