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

        public Guid OrderTypeId { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        [NotMapped]
        public OrderStatuses OrderStatusEnum
        {
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
                    case OrderStatusNames.PackingSlipCreated:
                        return OrderStatuses.PackingSlipCreated;
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
            }
        }

        [NotMapped]
        public string GlyphiconClass
        {
            get
            {
                switch (Name)
                {
                    case OrderStatusNames.Deleted:
                        return "glyphicon-trash";
                    case OrderStatusNames.InTransit:
                        return "glyphicon-plane";
                    case OrderStatusNames.Loaded:
                        return "glyphicon-gift";
                    case OrderStatusNames.PackingSlipCreated:
                        return "glyphicon-list-alt";
                    case OrderStatusNames.Pulled:
                        return "glyphicon-share-alt";
                    case OrderStatusNames.Received:
                        return "glyphicon-log-in";
                    case OrderStatusNames.SentToWarehouse:
                        return "glyphicon-send";
                    case OrderStatusNames.WarehouseReceived:
                        return "glyphicon-log-in";
                    default:
                        return "glyphicon-question-sign";
                }
            }
        }

        [NotMapped]
        public string Level
        {
            get
            {
                switch (Name)
                {
                    case OrderStatusNames.Deleted:
                        return "danger";
                    case OrderStatusNames.InTransit:
                        return "warning";
                    case OrderStatusNames.Loaded:
                        return "success";
                    case OrderStatusNames.PackingSlipCreated:
                        return "warning";
                    case OrderStatusNames.Pulled:
                        return "warning";
                    case OrderStatusNames.Received:
                        return "success";
                    case OrderStatusNames.SentToWarehouse:
                        return "info";
                    case OrderStatusNames.WarehouseReceived:
                        return "warning";
                    default:
                        return "default";
                }
            }
        }
    }
}
