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
            Result = (from status in context.OrderStatus
                let order = context.Orders.Where(o => o.OrderStatusId == status.Id)
                where status.OrderTypeId == _type.Id
                orderby status.SortOrder
                select (new OrderSummary {Status = status, Count = order.Count()})).ToList();
        }
    }
}
