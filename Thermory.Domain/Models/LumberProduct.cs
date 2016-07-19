using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Utils;

namespace Thermory.Domain.Models
{
    public class LumberProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid LumberTypeId { get; set; }

        public LumberType LumberType { get; set; }

        [Column("Length")]
        public int LengthInMillimeters { get; set; }

        public int Quantity { get; set; }

        public bool IncludeInCalculations { get; set; }

        [NotMapped]
        public double LengthInInches
        {
            get { return LengthConverter.ConvertMillimetersToInches(LengthInMillimeters); }
        }

        [NotMapped]
        public double LengthInFeet
        {
            get { return LengthConverter.ConvertMillimetersToFeet(LengthInMillimeters); }
        }

        [NotMapped]
        public string PacksHtml
        {
            get { return LumberType == null ? "" : HtmlHelpers.GetPacksHtml(Quantity, LumberType.LumberSubCategory.BundleSize); }
        }

        [NotMapped]
        public double TallyPercentage
        {
            get { return LumberType == null || LumberType.TotalLinearFeet == 0 ? 0 : Math.Round(LinearFeet / LumberType.TotalLinearFeet * 100, 0); }
        }

        [NotMapped]
        public double LinearFeet
        {
            get { return LumberCalculations.GetLinearFeet(Quantity, LengthInInches); }
        }

        [NotMapped]
        public double SquareFeet
        {
            get
            {
                return LumberType == null ? 0 : LumberCalculations.GetSquareFeet(LinearFeet, LumberType.LumberSubCategory.WidthInInches);
            }
        }
    }
}
