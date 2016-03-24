using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Tools
{
    public static class OrderHelper
    {
        public static IEnumerable<OrderLumberLineItem> GetCreatedOrderLumberLineItems(Order order,
            IEnumerable<OrderLumberLineItem> updatedLumberLineItems)
        {
            return
                updatedLumberLineItems.Where(
                    i => order.OrderLumberLineItems.All(li => li.LumberProductId != i.LumberProductId));
        }

        public static IEnumerable<OrderLumberLineItem> GetEditedOrderLumberLineItems(Order order,
            IEnumerable<OrderLumberLineItem> updatedLumberLineItems)
        {
            return
                updatedLumberLineItems.Where(
                    i => order.OrderLumberLineItems.Any(li => li.LumberProductId == i.LumberProductId));
        }

        public static IEnumerable<OrderLumberLineItem> GetDeletedOrderLumberLineItems(Order order,
            IEnumerable<OrderLumberLineItem> updatedLumberLineItems)
        {
            return order.OrderLumberLineItems.Where(
                i => updatedLumberLineItems.All(li => li.LumberProductId != i.LumberProductId));
        }
    }
}
