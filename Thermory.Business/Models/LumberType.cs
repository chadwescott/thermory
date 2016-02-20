using System;
using System.Collections.Generic;
using System.Linq;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberType : ILumberType
    {
        public Guid Id { get; set; }

        public ILumberSubCategory LumberSubCategory { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public int TotalPieces { get { return LumberProducts.Sum(lp => lp.Quantity); } }

        public double TotalLinearFeet { get { return LumberProducts.Sum(lp => lp.LinearFeet); } }

        public double TotalSquareFeet { get { return LumberProducts.Sum(lp => lp.SquareFeet); } }

        public int[] LumberLengthsMillimeters { get { return LumberProducts.Select(lp => lp.LengthInMillmeters).ToArray(); } }

        public double[] LumberLengthsFeet { get { return LumberProducts.Select(p => LengthConverter.ConvertMillimetersToFeet(p.LengthInMillmeters)).ToArray(); } }

        public IList<ILumberProduct> LumberProducts { get; set; }
    }
}
