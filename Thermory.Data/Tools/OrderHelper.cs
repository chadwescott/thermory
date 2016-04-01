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
            return order.OrderLumberLineItems == null
                ? updatedLumberLineItems
                : updatedLumberLineItems.Where(
                    i => order.OrderLumberLineItems.All(li => li.LumberProductId != i.LumberProductId));
        }

        public static IEnumerable<OrderLumberLineItem> GetEditedOrderLumberLineItems(Order order,
            IEnumerable<OrderLumberLineItem> updatedLumberLineItems)
        {
            return
                updatedLumberLineItems.Where(
                    i =>
                        order.OrderLumberLineItems.Any(
                            li => li.LumberProductId == i.LumberProductId && li.Quantity != i.Quantity));
        }

        public static IEnumerable<OrderMiscellaneousLineItem> GetCreatedOrderMiscellaneousLineItems(Order order,
            IEnumerable<OrderMiscellaneousLineItem> updatedMiscellaneousLineItems)
        {
            return order.OrderMiscellaneousLineItems == null
                ? updatedMiscellaneousLineItems
                : updatedMiscellaneousLineItems.Where(
                    i =>
                        order.OrderMiscellaneousLineItems.All(
                            li => li.MiscellaneousProductId != i.MiscellaneousProductId));
        }

        public static IEnumerable<OrderMiscellaneousLineItem> GetEditedOrderMiscellaneousLineItems(Order order,
            IEnumerable<OrderMiscellaneousLineItem> updatedMiscellaneousLineItems)
        {
            return
                updatedMiscellaneousLineItems.Where(
                    i =>
                        order.OrderMiscellaneousLineItems.Any(
                            mi => mi.MiscellaneousProductId == i.MiscellaneousProductId && mi.Quantity != i.Quantity));
        }
    }
}
