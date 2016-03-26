using Thermory.Data.Extensions;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class DeleteLumberLineItemBuilder : CommandBuilder
    {
        public DeleteLumberLineItemBuilder(Order order)
        {
            if (order.OrderLumberLineItems == null) return;
            var deleteOrderLumberLinesCommands = order.OrderLumberLineItems.MakeDeleteOrderLumberLineItemCommands();
            Commands.AddRange(deleteOrderLumberLinesCommands);
        }
    }
}
