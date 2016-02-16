using System;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberProduct : ILumberProduct
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public int LengthInMillmeters { get; set; }

        public double LengthInInches { get { return LengthConverter.ConvertMillimetersToInches(LengthInMillmeters); } }

        public double TallyPercentage { get { return Math.Round(LinearFeet / LumberType.TotalLinearFeet * 100, 0); } }

        public ILumberType LumberType { get; set; }

        public double LinearFeet { get { return Math.Round(Quantity * LengthInInches / 12, 0); } }

        public double SquareFeet
        {
            get { return Math.Round(LinearFeet * LumberType.LumberSubCategory.WidthInInches / 12, 0); }
        }
    }
}
