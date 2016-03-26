using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Domain.Models;

namespace Thermory.Data.Extensions
{
    internal static class OrderLumberLineItemsExt
    {
        public static IEnumerable<CreateOrderLumberLineItem> MakeCreateOrderLumberLineItemCommands(
            this IEnumerable<OrderLumberLineItem> createdLumberLineItems, Order order)
        {
            return
                createdLumberLineItems.Select(i => new CreateOrderLumberLineItem(order, i.LumberProductId, i.Quantity));
        }

        public static IEnumerable<EditOrderLumberLineItem> MakeEditOrderLumberLineItemCommands(
            this IEnumerable<OrderLumberLineItem> editedLumberLineItems)
        {
            return editedLumberLineItems.Select(i => new EditOrderLumberLineItem(i));
        }

        public static IEnumerable<DeleteOrderLumberLineItem> MakeDeleteOrderLumberLineItemCommands(
            this IEnumerable<OrderLumberLineItem> deletedLumberLineItems)
        {
            return deletedLumberLineItems.Select(i => new DeleteOrderLumberLineItem(i));
        }
    }
}
