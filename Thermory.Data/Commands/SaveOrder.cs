using System;
using System.Data.Entity;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveOrder : DatabaseCommand
    {
        private readonly Order _order;

        public SaveOrder(Order order)
        {
            _order = order;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            _order.CustomerId = _order.Customer == null ? null : (Guid?)_order.Customer.Id;
            _order.PackagingTypeId = _order.PackagingType == null ? null : (Guid?)_order.PackagingType.Id;
            _order.OrderTypeId = _order.OrderType.Id;
            
            _order.Customer = null;
            _order.OrderStatus = null;
            _order.OrderType = null;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_order.Id == Guid.Empty)
                context.Orders.Add(_order);
            else
                context.Entry(_order).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
