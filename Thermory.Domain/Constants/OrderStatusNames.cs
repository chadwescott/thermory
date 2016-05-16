using Thermory.Domain.Enums;

namespace Thermory.Domain.Constants
{
    public class OrderStatusNames
    {
        public const string Deleted = "Deleted";
        public const string InTransit = "In transit";
        public const string Loaded = "Loaded";
        public const string PackagingSlipCreated = "Packaging slip created";
        public const string Pulled = "Pulled";
        public const string Received = "Received";
        public const string SentToWarehouse = "Sent to warehouse";
        public const string WarehouseReceived = "Warehouse received";

        public static string GetOrderStatusName(OrderStatuses orderStatus)
        {
            switch (orderStatus)
            {
                case OrderStatuses.Deleted:
                    return Deleted;
                case OrderStatuses.InTransit:
                    return InTransit;
                case OrderStatuses.Loaded:
                    return Loaded;
                case OrderStatuses.PackagingSlipCreated:
                    return PackagingSlipCreated;
                case OrderStatuses.Pulled:
                    return Pulled;
                case OrderStatuses.Received:
                    return Received;
                case OrderStatuses.SentToWarehouse:
                    return SentToWarehouse;
                case OrderStatuses.WarehouseReceived:
                    return WarehouseReceived;
                default:
                    return "Unknown";
            }
        }
    }
}
