using System.Data.Entity;
using System;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetOrderById : DatabaseGetCommand<Order>
    {
        private readonly Guid _id;

        public GetOrderById(Guid id)
        {
            _id = id;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            Result =
                context.Orders.Include(o => o.OrderLumberLineItems)
                    .Include(o => o.OrderMiscellaneousLineItems)
                    .Include(o => o.OrderType)
                    .SingleOrDefault(o => o.Id == _id);
        }
    }
}
