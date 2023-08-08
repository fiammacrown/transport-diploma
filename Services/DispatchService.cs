using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Abeslamidze_Kursovaya7.Models;

namespace Abeslamidze_Kursovaya7.Services
{
    public class DispatchService
    {

        public Delivery DispatchOrders(List<Order> orders, List<Transport> transports)
        {
            // TODO
            // 1. Filter Registered orders
            // 2. Filter Free transports
            // 3. Group Orders with same From, To
            // 4. Iterate over grouped orders to calculate max subsum that <= transport Volume
            // 5. Assign transport to orders: set trasport status, set order status
            // 6. Calculate Delivery time and Total price
            // 7. Set Delivery timer to calculated Delivery time 
            // 8. Push Delivery to RunLoop

            var filteredOrders = FilterRegisteredOrders(orders);
            var filteredTransports = FilteFreeTransport(transports);

            var groupedOrders = GroupOrdersByFromTo(filteredOrders);

            foreach (var item in groupedOrders)
            {
                foreach (var order in item.Orders)
                {
                }
            }

            return new Delivery();
        }

        private List<Order> FilterRegisteredOrders(List<Order> orders)
        {
            return orders.Where(o => o.Status == OrderStatus.Registered).ToList();

        }

        private List<Transport> FilteFreeTransport(List<Transport> transports)
        {
            return transports.Where(t => t.Status == TransportStatus.Free).ToList();

        }

        private List<GroupedOrder> GroupOrdersByFromTo(List<Order> orders)
        {
            return orders
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
