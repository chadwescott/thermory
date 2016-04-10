using Thermory.Domain.Enums;

namespace Thermory.Domain.Constants
{
    public class OrderTypeNames
    {
        public const string PurchaseOrder = "Purchase";

        public const string SalesOrder = "Sales";

        public static string GetOrderTypeName(OrderTypes orderType)
        {
            switch (orderType)
            {
                case OrderTypes.PurchaseOrder:
                    return PurchaseOrder;
                case OrderTypes.SalesOrder:
                    return SalesOrder;
                default:
                    return "Unknown";
            }
        }
    }
}
