using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using Abeslamidze_Kursovaya7.Models;

namespace Abeslamidze_Kursovaya7.Services
{
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

    public class DispatchService
    {
        private List<Delivery> _inProgressDeliveries = new List<Delivery>();
        private List<Order> _inQueueOrders = new List<Order>();

        private List<Order> _orders;
        private List<Transport> _transports;

        private Dictionary<Distance, Transport> _temp = new Dictionary<Distance, Transport>();
        public DispatchService(List<Order> orders, List<Transport> transports)
        {
            _orders = orders;
            _transports = transports;
        }

        public DispatchServiceResult Dispatch()
        {
            DispatchGroupOrders();
            // если групповой заказ превышаем максимальный доступный объем машины
            // пробуем распределить заявки поодиночке начиная с наибольшей по весу
            DispatchOrders();
            // переводим грузоперевозки и транспорт в новый статус
            PutDeliveriesInProgress();
            // рассчитываем дату выполнения и стоимость для грузоперевозок и заявок
            CalculateDeliveryDateAndPrice();
            CalculateOrderDeliveryDateAndPrice();
            // необработанные заявки поступают в очередь
            InQueueOrders();
            return new DispatchServiceResult(_inProgressDeliveries, _inQueueOrders, FilteFreeTransport());
        }

        public void CalculateDeliveryDateAndPrice()
        {   
            foreach (var delivery in _inProgressDeliveries)
            {
                // срок выполнения = расстояние / скорость
                int distanceInKm = new Distance(delivery.From, delivery.To).InKm;
                double transportSpeedInKmHour = _transports.Where(t => t.Id == delivery.TransportId).Select(t => t.Speed).First();
                double deliveryTimeInHours = distanceInKm / transportSpeedInKmHour;

                // установить дату доставки используя минуты для ускорения
                delivery.EndDate = delivery.StartDate.AddMinutes(deliveryTimeInHours);

                // стоимость = расстояние * стоимость перевозки для транспортного средства
                double transportPricePerKm = _transports.Where(t => t.Id == delivery.TransportId).Select(t => t.PricePerKm).First();
                delivery.TotalPrice = distanceInKm * transportPricePerKm;
            }       

        }
        public void CalculateOrderDeliveryDateAndPrice()
        {
            foreach (var delivery in _inProgressDeliveries)
            {
                foreach (var orderId in delivery.OrderIds)
                {
                    Order order = _orders.Where(o => o.Id == orderId).First();

                    order.DeliveryDate = delivery.EndDate;
                    order.DeliveryPrice = delivery.TotalPrice / delivery.OrderIds.Count;
                }
            }

        }

        private void DispatchOrders()
        {
            var filteredOrders = FilterRegisteredOrders()
                .OrderByDescending(o => o.Weight).ToList();

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
                    var filteredTransports = FilteFreeTransport().Where(t => t.AssignedOrders.Count == 0)
                        .OrderByDescending(t => t.Volume);

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
            var groupedOrders = GroupOrdersByFromTo()
                .OrderByDescending(o => o.TotalWeight).ToList();

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
                    var filteredTransports = FilteFreeTransport().Where(t => t.AssignedOrders.Count == 0)
                        .OrderByDescending(t => t.Volume);

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
        private void PutDeliveriesInProgress()
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

                _inProgressDeliveries.Add(delivery);

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

        private void AssignOrders(List<Order> orders)
        {
            foreach (var order in orders)
            {
                order.Status = OrderStatus.Assigned;
            }
        }
        private void InQueueOrders()
        {
            var unprocessedOrders = _orders.Where(o => o.Status == OrderStatus.Registered);

            foreach (var order in unprocessedOrders)
            {
                order.Status = OrderStatus.InQueue;
                _inQueueOrders.Add(order);
            }

        }

        private List<Order> FilterRegisteredOrders()
        {
            return _orders.Where(o => o.Status == OrderStatus.Registered).ToList();

        }

        private List<Transport> FilteFreeTransport()
        {
            return _transports.Where(t => t.Status == TransportStatus.Free).ToList();

        }

        private List<GroupedOrder> GroupOrdersByFromTo()
        {
            return FilterRegisteredOrders()
                .GroupBy(order => new Distance(order.From, order.To))
                .Select(groupedOrder => new GroupedOrder(
                    groupedOrder.Key.From,
                    groupedOrder.Key.To,
                    groupedOrder.ToList()
                    )
                )
                .ToList();
        }
    }
}
