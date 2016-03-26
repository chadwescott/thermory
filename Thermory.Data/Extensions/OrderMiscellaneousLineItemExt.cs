using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Domain.Models;

namespace Thermory.Data.Extensions
{
    internal static class OrderMiscellaneousLineItemsExt
    {
        public static IEnumerable<CreateOrderMiscellaneousLineItem> MakeCreateOrderMiscellaneousLineItemCommands(
            this IEnumerable<OrderMiscellaneousLineItem> createdMiscellaneousLineItems, Order order)
        {
            return
                createdMiscellaneousLineItems.Select(
                    i => new CreateOrderMiscellaneousLineItem(order, i.MiscellaneousProductId, i.Quantity));
        }

        public static IEnumerable<EditOrderMiscellaneousLineItem> MakeEditOrderMiscellaneousLineItemCommands(
            this IEnumerable<OrderMiscellaneousLineItem> editedMiscellaneousLineItems)
        {
            return editedMiscellaneousLineItems.Select(i => new EditOrderMiscellaneousLineItem(i));
        }

        public static IEnumerable<DeleteOrderMiscellaneousLineItem> MakeDeleteOrderMiscellaneousLineItemCommands(
            this IEnumerable<OrderMiscellaneousLineItem> deletedMiscellaneousLineItems)
        {
            return deletedMiscellaneousLineItems.Select(i => new DeleteOrderMiscellaneousLineItem(i));
        }
    }
}
