using Thermory.Data.Extensions;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class DeleteMiscellaneousLineItemBuilder : CommandBuilder
    {
        public DeleteMiscellaneousLineItemBuilder(Order order)
        {
            if (order.OrderMiscellaneousLineItems == null) return;
            var deleteOrderMiscellaneousLinesCommands = order.OrderMiscellaneousLineItems.MakeDeleteOrderMiscellaneousLineItemCommands();
            Commands.AddRange(deleteOrderMiscellaneousLinesCommands);
        }
    }
}
