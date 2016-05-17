using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data
{
    public static class OrderStatusCache
    {
        private static readonly List<OrderStatus> OrderStatuses = DatabaseCommandDirectory.Instance.GetAllOrderStatuses().ToList();

        public static OrderStatus GetByOrderStatusEnum(OrderStatuses orderStatus)
        {
            return OrderStatuses.SingleOrDefault(s => s.OrderStatusEnum == orderStatus);
        }
    }
}
