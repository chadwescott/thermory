using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Thermory.Domain.Utils;

namespace Thermory.Domain.Models
{
    public class LumberType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid LumberSubCategoryId { get; set; }

        [ForeignKey("LumberSubCategoryId")]
        public LumberSubCategory LumberSubCategory { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        [ForeignKey("LumberTypeId")]
        public List<LumberProduct> LumberProducts { get; set; }

        [NotMapped]
        public int TotalPieces { get { return LumberProducts.Sum(lp => lp.Quantity); } }

        [NotMapped]
        public double TotalLinearFeet { get { return LumberProducts.Sum(lp => lp.LinearFeet); } }

        [NotMapped]
        public double TotalSquareFeet { get { return LumberProducts.Sum(lp => lp.SquareFeet); } }

        [NotMapped]
        public int[] LengthsMillimeters { get { return LumberProducts.Select(lp => lp.LengthInMillimeters).ToArray(); } }

        [NotMapped]
        public double[] LengthsFeet { get { return LumberProducts.Select(p => LengthConverter.ConvertMillimetersToFeet(p.LengthInMillimeters)).ToArray(); } }
    }
}
