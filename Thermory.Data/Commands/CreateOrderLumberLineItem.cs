using System;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class CreateOrderLumberLineItem : DatabaseCommand
    {
        private readonly Order _order;
        private readonly Guid _lumberProductId;
        private readonly int _quantity;

        public CreateOrderLumberLineItem(Order order, Guid lumberProductId, int quantity)
        {
            _order = order;
            _lumberProductId = lumberProductId;
            _quantity = quantity;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            var lineItem = new OrderLumberLineItem
            {
                OrderId = _order.Id,
                LumberProductId = _lumberProductId,
                Quantity = _quantity
            };
            context.OrderLumberLineItems.Add(lineItem);
            context.SaveChanges();
        }
    }
}
