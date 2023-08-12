﻿using System.Collections.Generic;
using System.Linq;

using Abeslamidze_Kursovaya7.Repos;
using Abeslamidze_Kursovaya7.Models;

namespace Abeslamidze_Kursovaya7.Services
{
    public class DispatchService
    {
        private readonly OrdersRepo _ordersRepo;
        private readonly DeliveriesRepo _deliveriesRepo;
        private readonly TransportsRepo _transportsRepo;

        private Dictionary<Distance, Transport> _temp = new Dictionary<Distance, Transport>();
        public DispatchService(OrdersRepo ordersService, TransportsRepo transportsRepo, DeliveriesRepo deliveriesRepo)
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
            UpdateDeliveriesStatus();
            // рассчитываем дату выполнения и стоимость для грузоперевозок и заявок
            UpdateDeliveryDateAndPrice();
            UpdateOrderDeliveryDateAndPrice();
            // необработанные заявки поступают в очередь
            UpdateOrders();
            return new DispatchServiceResult(_deliveriesRepo.GetAll(), _ordersRepo.GetInQueue(), _transportsRepo.GetFree());
        }

        public void UpdateDeliveryDateAndPrice()
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
        public void UpdateOrderDeliveryDateAndPrice() // TODO: rework
        {
            foreach (var delivery in _deliveriesRepo.GetAll())
            {
                foreach (var orderId in delivery.OrderIds)
                {
                    var order = _ordersRepo.GetById(orderId);
                    if (order != null)
                    {
                        order.DeliveryDate = delivery.EndDate;
                        order.DeliveryPrice = delivery.TotalPrice / delivery.OrderIds.Count;
                    }
                }
            }

        }

        private void DispatchOrders()
        {
            var filteredOrders = _ordersRepo.GetRegisteredOrders();

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
                        }

                    }

                }
            }
        }

        private void DispatchGroupOrders()
        {
            var groupedOrders = _ordersRepo.GetRegisteredOrdersGroupByFromTo();

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
                        }

                    }

                }
            }

        }
        private void UpdateDeliveriesStatus()
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
        private void UpdateOrders()
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

    }

    public class DispatchServiceResult
    {
        public DispatchServiceResult(List<Delivery> inProgressDeliveries, List<Order> inQueueOrders, List<Transport> freeTransport)
        {
            InProgressDeliveries = inProgressDeliveries;
            InQueueOrders = inQueueOrders;
            FreeTransport = freeTransport;
        }

        public List<Delivery> InProgressDeliveries { get; }
        public List<Order> InQueueOrders { get; }
        public List<Transport> FreeTransport { get; }

        public int NumOfInProgressDeliveries { get => InProgressDeliveries.Count; }
        public int NumOfInQueueOrders { get => InQueueOrders.Count; }
        public int NumOfFreeTransport { get => FreeTransport.Count; }
    }
}
