using System.Collections.Generic;
using System.Linq;

using Abeslamidze_Kursovaya7.Repos;
using Abeslamidze_Kursovaya7.Models;
using System;
using Abeslamidze_Kursovaya7.Interfaces;
using Abeslamidze_Kursovaya7.ViewModels;
using System.Windows.Controls;

namespace Abeslamidze_Kursovaya7.Services
{
    public class DispatchService
    {
        public UnitOfWork unitOfWork;
        public DispatchService(UnitOfWork u)
        {
            unitOfWork = u;

        }

        public void Dispatch()
        {
            DispatchOrders();
            unitOfWork.Save();

            var a = unitOfWork.DeliveryRepository.GetNew();
            var b = unitOfWork.OrderRepository.GetInQueue();
            var d = unitOfWork.TransportRepository.GetFree();

        }

        public void Start()
        {
            StartDeliveries();
            unitOfWork.Save();

            var a = unitOfWork.DeliveryRepository.GetInProgress();
            var b = unitOfWork.OrderRepository.GetInQueue();
            var d = unitOfWork.TransportRepository.GetFree();

        }

        public void Update()
        {
            UpdateDeliveries();
            unitOfWork.Save();

            var a = unitOfWork.DeliveryRepository.GetInProgress();
            var b = unitOfWork.OrderRepository.GetInQueue();
            var d = unitOfWork.TransportRepository.GetFree();
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
                    delivery.Transport.Done();

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
                foreach (var transport in freeTransport)
                {
                    if (order.Weight <= transport.AvailableVolume)
                    {
                        unitOfWork.DeliveryRepository.Add(new Delivery(
                            order.From,
                            order.To,
                            order,
                            transport)
                        );

                        transport.Load(order);
                        unitOfWork.TransportRepository.Update(transport);

                        order.Assign();
                        unitOfWork.OrderRepository.Update(order);

                        break;
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
