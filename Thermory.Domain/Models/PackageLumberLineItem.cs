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

        [NotMapped]
        public double Weight
        {
            get
            {
                return LumberProduct == null || LumberProduct.LumberType == null ||
                       LumberProduct.LumberType.LumberSubCategory == null ||
                       LumberProduct.LumberType.LumberSubCategory.Weight == null
                    ? 0
                    : Math.Round(LinearFeet * LumberProduct.LumberType.LumberSubCategory.Weight.Value * Quantity, 2);
            }
        }
    }
}
