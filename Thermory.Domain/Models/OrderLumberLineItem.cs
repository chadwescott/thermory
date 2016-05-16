using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
            get
            {
                if (Quantity == 0) return "0";
                var bundleSize = LumberProduct.LumberType.LumberSubCategory.BundleSize;
                var fullPacks = Quantity / bundleSize;
                var fraction = Quantity % bundleSize == 0 ? "" : string.Format("<sup>{0}</sup>&frasl;<sub>{1}</sub>", Quantity % bundleSize, bundleSize);
                var fullPackString = fullPacks == 0 && fraction != string.Empty ? "" : fullPacks.ToString();
                return string.Format("{0}{1}", fullPackString, fraction);
            }
        }

        [NotMapped]
        public double LinearFeet { get { return LumberProduct.LengthInInches * Quantity; } }

        [NotMapped]
        public double SquareFeet { get { return Math.Round(LinearFeet * LumberProduct.LumberType.LumberSubCategory.WidthInInches / 12, 0); } }
    }
}
