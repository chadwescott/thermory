using System;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Models;
using Thermory.Web.Models.Status;

namespace Thermory.Web.Models
{
    [Table("Order_V")]
    public class OrderView : IViewModel
    {
        [Column("OrderId")]
        public string recid { get; set; }

        [Column("OrderNumber")]
        public string OrderNumber { get; set; }

        [Column("OrderType")]
        public string OrderType { get; set; }

        [Column("CustomerName")]
        public string CustomerName { get; set; }

        [Column("CreatedBy")]
        public string CreatedBy { get; set; }

        [Column("CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        private StatusGroup _orderStatus;

        [NotMapped]
        public StatusGroup OrderStatus
        {
            get { return _orderStatus ?? (_orderStatus = new OrderStatusGroup(Status)); }
        }
    }
}