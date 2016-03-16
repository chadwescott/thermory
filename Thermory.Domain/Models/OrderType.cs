using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Constants;
using Thermory.Domain.Enums;

namespace Thermory.Domain.Models
{
    public class OrderType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public OrderTypes OrderTypeEnum
        {
            get { return Name == OrderTypeNames.PurchaseOrder ? OrderTypes.Purchase : OrderTypes.Sales; }
        }
    }
}
