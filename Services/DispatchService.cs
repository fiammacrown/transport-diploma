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
    public class DispatchService
    {
        private List<Delivery> _inProgressDeliveries = new List<Delivery>();
        private List<Order> _inQueueOrders = new List<Order>();

        private List<Order> _orders;
        private List<Transport> _transports;
        public DispatchService(List<Order> orders, List<Transport> transports)
        {
            _orders = orders;
            _transports = transports;
        }

        public int NumOfInProgressDeliveries { get => _inProgressDeliveries.Count; }
        public int NumOfFreeTransport { get => FilteFreeTransport().Count; }
        public int NumOfInQueueOrders { get => _inQueueOrders.Count; }
        public List<Delivery> Dispatch()
        {
            DispatchGroupOrders();
            // если групповой заказ превышаем максимальный доступный объем машины
            // пробуем распределить заявки поодиночке начиная с наибольшей по весу
            DispatchOrders();
            // необработанные заявки поступают в очередь
            InQueueOrders();
            return _inProgressDeliveries;
        }

        public void CalculateDeliveryPrice(Delivery delivery)
        {
            
        }
        public void CalculateOrderPrice(Order order)
        {

        }

        public void GetDeliveryDate(Delivery delivery)
        {

        }
        public void GetOrderDate(Order order)
        {

        }

        private void DispatchOrders()
        {
            var filteredOrders = FilterRegisteredOrders().OrderBy(o => o.Weight);
            var filteredTransports = FilteFreeTransport().OrderBy(t => t.Volume);

            var temp = new Dictionary<string, Transport>();

            foreach (var item in filteredOrders)
            {
                string key = item.From.ToString() + "-" + item.To.ToString();

                

                if (temp.TryGetValue(key, out var value))
                {
                    if (item.Weight <= value.AvailableVolume)
                    {
                        value.Load(item);

                        item.Status = OrderStatus.Assigned;

                    }
                }
                else
                {
                    foreach (var transport in filteredTransports)
                    {
                        if (item.Weight <= transport.AvailableVolume)
                        {
                            transport.Load(item);

                            item.Status = OrderStatus.Assigned;

                            temp.Add(key, transport);
                        }

                    }

                }
            }

            foreach (KeyValuePair<string, Transport> kvp in temp)
            {
                string[] words = kvp.Key.Split('-');
                Transport transport = kvp.Value;

                var delivery = new Delivery(
                        new Location(words[0]),
                        new Location(words[1]),
                        transport.AssignedOrders,
                        transport.Id
                    );

                _inProgressDeliveries.Add(delivery);
            }

            return;
        }

        private void DispatchGroupOrders()
        {
            var groupedOrders = GroupOrdersByFromTo().OrderBy(g => g.TotalWeight);
            var filteredTransports = FilteFreeTransport().OrderBy(t => t.Volume);

            var temp = new Dictionary<string, Transport>();

            foreach (var item in groupedOrders)
            {

                string key = item.From.ToString() + "-" + item.To.ToString();

                if (temp.TryGetValue(key, out var value))
                {
                    if (item.TotalWeight <= value.AvailableVolume)
                    {
                        LoadTransport(value, item.Orders);
                        AssignOrders(item.Orders);

                    }
                }
                else
                {
                    foreach (var transport in filteredTransports)
                    {
                        if (item.TotalWeight <= transport.AvailableVolume)
                        {
                            LoadTransport(transport, item.Orders);
                            AssignOrders(item.Orders);

                            temp.Add(key, transport);
                        }

                    }

                }
            }

            foreach (KeyValuePair<string, Transport> kvp in temp)
            {
                string[] words = kvp.Key.Split('-');
                Transport transport = kvp.Value;

                var delivery = new Delivery(
                        new Location(words[0]),
                        new Location(words[1]),
                        transport.AssignedOrders,
                        transport.Id
                    );

                _inProgressDeliveries.Add(delivery);
            }

            return;
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
                .GroupBy(order => new { order.From, order.To })
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
