using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Constants;
using Thermory.Domain.Enums;

namespace Thermory.Domain.Models
{
    public class OrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public OrderStatuses OrderStatusEnum {
            get
            {
                switch (Name)
                {
                    case OrderStatusNames.Deleted:
                        return OrderStatuses.Deleted;
                    case OrderStatusNames.InTransit:
                        return OrderStatuses.InTransit;
                    case OrderStatusNames.Loaded:
                        return OrderStatuses.Loaded;
                    case OrderStatusNames.PackagingSlipCreated:
                        return OrderStatuses.PackagingSlipCreated;
                    case OrderStatusNames.Pulled:
                        return OrderStatuses.Pulled;
                    case OrderStatusNames.Received:
                        return OrderStatuses.Received;
                    case OrderStatusNames.SentToWarehouse:
                        return OrderStatuses.SentToWarehouse;
                    case OrderStatusNames.WarehouseReceived:
                        return OrderStatuses.WarehouseReceived;
                    default:
                        return OrderStatuses.Unknown;
                }
            } }
    }
}
