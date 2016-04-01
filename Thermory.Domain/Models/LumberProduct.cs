using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public double LengthInInches
        {
            get { return LengthConverter.ConvertMillimetersToInches(LengthInMillimeters); }
        }

        [NotMapped]
        public double TallyPercentage
        {
            get { return LumberType == null ? 0 : Math.Round(LinearFeet/LumberType.TotalLinearFeet*100, 0); }
        }

        [NotMapped]
        public double LinearFeet
        {
            get { return Math.Round(Quantity*LengthInInches/12, 0); }
        }

        [NotMapped]
        public double SquareFeet
        {
            get
            {
                return LumberType == null ? 0 : Math.Round(LinearFeet*LumberType.LumberSubCategory.WidthInInches/12, 0);
            }
        }
    }
}
