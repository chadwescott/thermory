using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Utils;

namespace Thermory.Domain.Models
{
    public class PackageLumberLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid PackageId { get; set; }

        [ForeignKey("PackageId")]
        public Package Package { get; set; }

        public Guid LumberProductId { get; set; }

        [ForeignKey("LumberProductId")]
        public LumberProduct LumberProduct { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public string PacksHtml
        {
            get
            {
                return LumberProduct == null
                    ? ""
                    : HtmlHelpers.GetPacksHtml(Quantity, LumberProduct.LumberType.LumberSubCategory.BundleSize);
            }
        }

        [NotMapped]
        public double LinearFeet
        {
            get { return LumberProduct == null ? 0 : LumberCalculations.GetLinearFeet(Quantity, LumberProduct.LengthInInches); }
        }

        [NotMapped]
        public double SquareFeet
        {
            get
            {
                return LumberProduct == null
                    ? 0
                    : LumberCalculations.GetSquareFeet(LinearFeet, LumberProduct.LumberType.LumberSubCategory.WidthInInches);
            }
        }
    }
}
