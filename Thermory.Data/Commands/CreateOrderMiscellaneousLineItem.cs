using System;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class CreateOrderMiscellaneousLineItem : DatabaseContextCommand
    {
        private readonly Order _order;
        private readonly Guid _miscellaneousProductId;
        private readonly int _quantity;

        public CreateOrderMiscellaneousLineItem(Order order, Guid miscellaneousProductId, int quantity)
        {
            _order = order;
            _miscellaneousProductId = miscellaneousProductId;
            _quantity = quantity;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            var lineItem = new OrderMiscellaneousLineItem
            {
                OrderId = _order.Id,
                MiscellaneousProductId = _miscellaneousProductId,
                Quantity = _quantity
            };
            context.OrderMiscellaneousLineItems.Add(lineItem);
            context.SaveChanges();
        }
    }
}
