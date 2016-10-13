using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetOrderStatusSummary : DatabaseGetCommand<List<OrderSummary>>
    {
        private readonly OrderType _type;

        public GetOrderStatusSummary(OrderTypes type)
        {
            _type = OrderTypeCache.GetByOrderTypeEnum(type);
        }

        protected override void OnExecute(ThermoryContext context)
        {
            Result =
                context.Orders
                    .Where(o => o.OrderType.Id == _type.Id)
                    .GroupBy(o => o.OrderStatus)
                    .Select(o => new OrderSummary {Status = o.Key, Count = o.Count()})
                    .OrderBy(o => o.Status.SortOrder)
                    .ToList();
        }
    }
}
