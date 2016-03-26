using System.Collections.Generic;
using Thermory.Data.Extensions;
using Thermory.Data.Tools;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class EditLumberLineItemBuilder : CommandBuilder
    {
        public EditLumberLineItemBuilder(Order order, IEnumerable<OrderLumberLineItem> updatedLumberLineItems)
        {
            var editedLumberLineItems = OrderHelper.GetEditedOrderLumberLineItems(order, updatedLumberLineItems);
            var editOrderLumberLinesCommands = editedLumberLineItems.MakeEditOrderLumberLineItemCommands();
            Commands.AddRange(editOrderLumberLinesCommands);
        }
    }
}
