using System.Collections.Generic;
using System.Linq;

using Abeslamidze_Kursovaya7.Repos;
using Abeslamidze_Kursovaya7.Models;
using System;
using Abeslamidze_Kursovaya7.Interfaces;

namespace Abeslamidze_Kursovaya7.Services
{
    public class DispatchService
    {
        private readonly OrdersRepo _ordersRepo;
        private readonly IDeliveriesRepo _deliveriesRepo;
        private readonly ITransportsRepo _transportsRepo;

        private Dictionary<Distance, Transport> _temp = new Dictionary<Distance, Transport>();
        public DispatchService(OrdersRepo ordersService, ITransportsRepo transportsRepo, IDeliveriesRepo deliveriesRepo)
        {
            _ordersRepo = ordersService;
            _deliveriesRepo = deliveriesRepo;
            _transportsRepo = transportsRepo;
        }

        public DispatchServiceResult Dispatch()
        {
            DispatchGroupOrders();
            // если групповой заказ превышаем максимальный доступный объем машины
            // пробуем распределить заявки поодиночке начиная с наибольшей по весу
            DispatchOrders();
            // переводим грузоперевозки и транспорт в новый статус
            InProgressDeliveryStatus();
            // рассчитываем дату выполнения и стоимость для грузоперевозок и заявок
            CalculateDeliveryDateAndPrice();
            CalculateOrderDeliveryDateAndPrice();
            // необработанные заявки поступают в очередь
            InQueueOrders();

            return new DispatchServiceResult(
                _deliveriesRepo.GetInProgress(),
                _ordersRepo.GetInQueue(),
                _ordersRepo.GetDeliverableOrders(),
                _transportsRepo.GetFree()
               );
        }

        public DispatchServiceResult Update()
        {
            foreach (var delivery in _deliveriesRepo.GetInProgress())
            {
                if (delivery.EndDate <= DateTime.Now)
                {
                    delivery.Status = DeliveryStatus.Done;

                    var transport = _transportsRepo.GetById(delivery.TransportId);
                    var orders = _ordersRepo.GetByIds(delivery.OrderIds);

                    UnloadTransport(transport!, orders);
                    transport!.Status = TransportStatus.Free;

                    InDoneOrders(orders);
                }
            }

            return new DispatchServiceResult(
                _deliveriesRepo.GetInProgress(),
                _ordersRepo.GetInQueue(),
                _ordersRepo.GetDeliverableOrders(),
                _transportsRepo.GetFree()
               );
        }
        public void CalculateDeliveryDateAndPrice()
        {   
            foreach (var delivery in _deliveriesRepo.GetAll())
            {
                // срок выполнения = расстояние / скорость
                int distanceInKm = new Distance(delivery.From, delivery.To).InKm;
                double transportSpeedInKmHour = _transportsRepo.GetSpeedInKmById(delivery.TransportId);
                double deliveryTimeInHours = distanceInKm / transportSpeedInKmHour;

                // установить дату доставки используя минуты для ускорения
                delivery.EndDate = delivery.StartDate.AddMinutes(deliveryTimeInHours);

                // стоимость = расстояние * стоимость перевозки для транспортного средства
                double transportPricePerKm = _transportsRepo.GetPricePerKmById(delivery.TransportId);
                delivery.TotalPrice = distanceInKm * transportPricePerKm;
            }       

        }
        public void CalculateOrderDeliveryDateAndPrice() 
        {
            foreach (var delivery in _deliveriesRepo.GetAll())
            {
                var orders = _ordersRepo.GetByIds(delivery.OrderIds);
                delivery.TotalWeight = orders.Sum(o => o.Weight);

                foreach (var order in orders)
                {
                    if (order != null)
                    {
                        order.DeliveryDate = delivery.EndDate;
                        order.DeliveryPrice = delivery.TotalPrice * ( order.Weight / delivery.TotalWeight );
                    }
                }
            }

        }

        private void DispatchOrders()
        {
            var filteredOrders = _ordersRepo.GetDeliverableOrders();

            foreach (var item in filteredOrders)
            {
                var key = new Distance(item.From, item.To);

                if (_temp.TryGetValue(key, out var value))
                {
                    if (item.Weight <= value.AvailableVolume)
                    {
                        value.Load(item);

                        item.Status = OrderStatus.Assigned;

                    }
                }
                else
                {
                    var filteredTransports = _transportsRepo.GetFree();

                    foreach (var transport in filteredTransports)
                    {
                        if (item.Weight <= transport.AvailableVolume)
                        {
                            transport.Load(item);

                            item.Status = OrderStatus.Assigned;

                            _temp.Add(key, transport);

                            break;
                        }

                    }

                }
            }
        }

        private void DispatchGroupOrders()
        {
            var groupedOrders = _ordersRepo.GetDeliverableOrdersGroupByFromTo();

            foreach (var item in groupedOrders)
            {
                var key = new Distance(item.From, item.To);

                if (_temp.TryGetValue(key, out var value))
                {
                    if (item.TotalWeight <= value.AvailableVolume)
                    {
                        LoadTransport(value, item.Orders);

                        AssignOrders(item.Orders);

                    }
                }
                else
                {
                    var filteredTransports = _transportsRepo.GetFree();

                    foreach (var transport in filteredTransports)
                    {
                        if (item.TotalWeight <= transport.AvailableVolume)
                        {
                            LoadTransport(transport, item.Orders);

                            AssignOrders(item.Orders);

                            _temp.Add(key, transport);

                            break;
                        }

                    }

                }
            }

        }
        private void InProgressDeliveryStatus()
        {
            foreach (KeyValuePair<Distance, Transport> kvp in _temp)
            {
                Distance distance = kvp.Key;
                Transport transport = kvp.Value;

                var delivery = new Delivery(
                        distance.From,
                        distance.To,
                        transport.AssignedOrders,
                        transport.Id
                    );

                _deliveriesRepo.Add(delivery);

                transport.Status = TransportStatus.InTransit;
            }
        }

        private void LoadTransport(Transport transport, List<Order> orders)
        {
            foreach (var order in orders)
            {
                transport.Load(order);
            }
        }

        private void UnloadTransport(Transport transport, List<Order> orders)
        {
            foreach (var order in orders)
            {
                transport.Unload(order);
            }
        }

        private void InQueueOrders()
        {
            var unprocessedOrders = _ordersRepo.GetRegisteredOrders();

            foreach (var order in unprocessedOrders)
            {
                order.Status = OrderStatus.InQueue;
            }

        }
        private void AssignOrders(List<Order> orders)
        {
            foreach (var order in orders)
            {
                order.Status = OrderStatus.Assigned;
            }
        }
        private void InDoneOrders(List<Order> orders)
        {
            foreach (var order in orders)
            {
                order.Status = OrderStatus.Done;
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
        private List<Order>  DeliverableOrders { get; }
        private List<Order> InQueueOrders { get; }
        private List<Transport> FreeTransport { get; }

        public int NumOfInProgressDeliveries { get => InProgressDeliveries.Count; }

        public int NumOfDeliverableOrders { get => DeliverableOrders.Count; }
        public int NumOfInQueueOrders { get => InQueueOrders.Count; }
        public int NumOfFreeTransport { get => FreeTransport.Count; }

        
    }
}
