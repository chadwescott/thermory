using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Utils;

namespace Thermory.Domain.Models
{
    public class LumberSubCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid LumberCategoryId { get; set; }

        [ForeignKey("LumberCategoryId")]
        public LumberCategory LumberCategory { get; set; }

        public string Name { get; set; }

        [Column("Width")]
        public int WidthInMillimeters { get; set; }

        [Column("Thickness")]
        public int ThicknessInMillimeters { get; set; }

        public int BundleSize { get; set; }

        public int SortOrder { get; set; }

        public decimal? Weight { get; set; }

        [ForeignKey("LumberSubCategoryId")]
        public List<LumberType> LumberTypes { get; set; }

        [NotMapped]
        public double WidthInInches { get { return LengthConverter.ConvertMillimetersToInches(WidthInMillimeters); } }

        [NotMapped]
        public double ThicknessInInches { get { return LengthConverter.ConvertMillimetersToInches(ThicknessInMillimeters); } }
    }
}
