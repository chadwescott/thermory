using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Thermory.Domain.Utils;

namespace Thermory.Domain.Models
{
    public class OrderLumberLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public Guid LumberProductId { get; set; }

        [ForeignKey("LumberProductId")]
        public LumberProduct LumberProduct { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public string PacksHtml
        {
            get { return HtmlHelpers.GetPacksHtml(Quantity, LumberProduct.LumberType.LumberSubCategory.BundleSize); }
        }

        [NotMapped]
        public double LinearFeet { get { return LumberCalculations.GetLinearFeet(Quantity, LumberProduct.LengthInInches); } }

        [NotMapped]
        public double SquareFeet { get { return LumberCalculations.GetSquareFeet(LinearFeet, LumberProduct.LumberType.LumberSubCategory.WidthInInches); } }

        [NotMapped]
        public double TallyPercentage
        {
            get { return Math.Round(LinearFeet / TotalLinearFeetForLumberType * 100, 0); }
        }

        [NotMapped]
        private double TotalLinearFeetForLumberType
        {
            get
            {
                return
                    Order.OrderLumberLineItems.Where(li => li.LumberProduct.LumberTypeId == LumberProduct.LumberTypeId)
                        .Sum(li => li.LinearFeet);
            }
        }
    }
}
