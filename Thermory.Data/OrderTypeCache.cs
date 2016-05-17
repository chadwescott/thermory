using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data
{
    public static class OrderTypeCache
    {
        private static readonly List<OrderType> OrderTypees = DatabaseCommandDirectory.Instance.GetAllOrderTypes().ToList();

        public static OrderType GetByOrderTypeEnum(OrderTypes orderType)
        {
            return OrderTypees.SingleOrDefault(s => s.OrderTypeEnum == orderType);
        }
    }
}
