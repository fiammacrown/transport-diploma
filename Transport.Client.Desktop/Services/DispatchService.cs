using System.Collections.Generic;
using Abeslamidze_Kursovaya7.Models;
using System;

namespace Abeslamidze_Kursovaya7.Services
{
    public class DispatchService
    {
        public UnitOfWork unitOfWork;

        public int NumOfNewDeliveries = 0;
        public int NumOfInProgressDeliveries = 0;
        public int NumOfDoneDeliveries = 0;

        public int NumOfInQueueOrders = 0;
        public int NumOfAssignedOrders = 0;
        public int NumOfInProgressOrders = 0;
        public int NumOfDoneOrders = 0;

        public int NumOfAssignedTransport = 0;
        public int NumOfIntransitTransport = 0;
        public int NumOfFreeTransport = 0;

        private Dictionary<Distance, Transport>  _temp = new Dictionary<Distance, Transport>();

        public DispatchService(UnitOfWork u)
        {
            unitOfWork = u;

        }

        public bool Dispatch()
        {
            var deliverableOrders = unitOfWork.OrderRepository.GetDeliverableOrders();
            var freeTransport = unitOfWork.TransportRepository.GetFree();

            if (deliverableOrders.Count == 0 || freeTransport.Count == 0)
            {
                return false;
            }

            DispatchOrders(deliverableOrders, freeTransport);

            return true;

        }

        public bool Start()
        {
            var newDeliveries = unitOfWork.DeliveryRepository.GetNew();
            if (newDeliveries.Count == 0)
            {
                return false;
            }

            StartDeliveries(newDeliveries);
            return true;

        }

        public bool Update()
        {
            var inProgressDeliveries = unitOfWork.DeliveryRepository.GetInProgress();

            if (inProgressDeliveries.Count == 0)
            {
                return false;
            }

            UpdateDeliveries(inProgressDeliveries);

            return true;
        }

        public void UpdateDeliveries(List<Delivery> inProgressDeliveries)
        {
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

                    NumOfDoneDeliveries += 1;
                    NumOfDoneOrders += 1;
                    NumOfFreeTransport += 1;


                }
            }

        }

        private void DispatchOrders(List<Order> deliverableOrders, List<Transport> freeTransport)
        {
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

                        newDelivery.CalculatePrice(value);
                        unitOfWork.DeliveryRepository.Add(newDelivery);

                        value.Load(order);
                        unitOfWork.TransportRepository.Update(value);

                        order.Assign();
                        unitOfWork.OrderRepository.Update(order);

                        NumOfNewDeliveries += 1;
                        NumOfAssignedOrders += 1;

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

                            newDelivery.CalculatePrice(transport);
                            unitOfWork.DeliveryRepository.Add(newDelivery);

                            transport.Assign();
                            transport.Load(order);
                            unitOfWork.TransportRepository.Update(transport);

                            order.Assign();
                            unitOfWork.OrderRepository.Update(order);

                            _temp.Add(distance, transport);

                            NumOfNewDeliveries += 1;
                            NumOfAssignedOrders += 1;

                            NumOfAssignedTransport += 1;

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

                    NumOfInQueueOrders += 1;
                }
            }
        }      
        
        private void StartDeliveries(List<Delivery> newDeliveries)
        {
            
            DateTime start = DateTime.Now;

            foreach (var delivery in newDeliveries)
            {
                var distance = new Distance(delivery.Order.From, delivery.Order.To);

                delivery.InProgress(distance, start);
                delivery.Order.InProgress();
                delivery.Transport.InTransit();

                unitOfWork.DeliveryRepository.Update(delivery);
                unitOfWork.OrderRepository.Update(delivery.Order);
                unitOfWork.TransportRepository.Update(delivery.Transport);

                NumOfInProgressDeliveries += 1;
                NumOfInProgressOrders += 1;
            }
        }
    }

    public class DispatchServiceResult
    {

        public int NumOfNewDeliveries { get; set; }
        public int NumOfInProgressDeliveries { get; set; }
        public int NumOfDoneDeliveries { get; set; }
        public int NumOfAssignedOrders { get; set; }
        public int NumOfInQueueOrders { get; set; }
        public int NumOfInProgressOrders { get; set; }
        public int NumOfDoneOrders{ get; set; }
        public int NumOfAssignedTransport { get; set; }


    }
}
