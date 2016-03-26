using System.Collections.Generic;
using Thermory.Data.Extensions;
using Thermory.Data.Tools;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class EditMiscellaneousLineItemBuilder : CommandBuilder
    {
        public EditMiscellaneousLineItemBuilder(Order order, IEnumerable<OrderMiscellaneousLineItem> updatedMiscellaneousLineItems)
        {
            var editedMiscellaneousLineItems = OrderHelper.GetEditedOrderMiscellaneousLineItems(order, updatedMiscellaneousLineItems);
            var editOrderMiscellaneousLinesCommands = editedMiscellaneousLineItems.MakeEditOrderMiscellaneousLineItemCommands();
            Commands.AddRange(editOrderMiscellaneousLinesCommands);
        }
    }
}
