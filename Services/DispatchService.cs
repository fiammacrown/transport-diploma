using System.Collections.Generic;
using Abeslamidze_Kursovaya7.Models;
using System;

namespace Abeslamidze_Kursovaya7.Services
{
    public class DispatchService
    {
        public UnitOfWork unitOfWork;

        public int NumOfInProgressDeliveries = 0;
        public int NumOfInQueueOrders = 0;

        private Dictionary<Distance, Transport>  _temp = new Dictionary<Distance, Transport>();

        public DispatchService(UnitOfWork u)
        {
            unitOfWork = u;

        }

        public DispatchServiceResult Dispatch()
        {
            DispatchOrders();
            unitOfWork.Save();

            return new DispatchServiceResult(
                NumOfInProgressDeliveries,
                NumOfInQueueOrders,
                unitOfWork.OrderRepository.GetDeliverableOrders().Count,
                unitOfWork.TransportRepository.GetFree().Count
            );

        }

        public void Start()
        {
            StartDeliveries();
            unitOfWork.Save();

        }

        public DispatchServiceResult Update()
        {
            UpdateDeliveries();
            unitOfWork.Save();

            return new DispatchServiceResult(
                unitOfWork.OrderRepository.GetDeliverableOrders().Count,
                unitOfWork.TransportRepository.GetFree().Count
            );
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

                        NumOfInProgressDeliveries += 1;

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

                            NumOfInProgressDeliveries += 1;

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

                    NumOfInQueueOrders += 1;

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
        public DispatchServiceResult(int inProgressDeliveries, int inQueueOrders, int deliverableOrders, int freeTransport)
        {
            NumOfInProgressDeliveries = inProgressDeliveries;
            NumOfDeliverableOrders = deliverableOrders;
            NumOfInQueueOrders = inQueueOrders;
            NumOfFreeTransport = freeTransport;
        }

        public DispatchServiceResult(int deliverableOrders, int freeTransport)
        {
            NumOfDeliverableOrders = deliverableOrders;
            NumOfFreeTransport = freeTransport;
        }

        public int NumOfInProgressDeliveries { get; }
        public int NumOfDeliverableOrders { get; }
        public int NumOfInQueueOrders { get; }
        public int NumOfFreeTransport { get; }


    }
}
