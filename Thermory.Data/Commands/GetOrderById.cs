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
                context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderType)
                    .Include(o => o.OrderStatus)
                    .Include(o => o.PackagingType)
                    .Include(o => o.Packages.Select(p => p.PackageLumberLineItems))
                    .Include(
                        o =>
                            o.OrderLumberLineItems.Select(
                                li => li.LumberProduct.LumberType.LumberSubCategory.LumberCategory))
                    .Include(
                        o =>
                            o.OrderMiscellaneousLineItems.Select(
                                li => li.MiscellaneousProduct.MiscellaneousSubCategory.MiscellaneousCategory))
                    .SingleOrDefault(o => o.Id == _id);
        }
    }
}
