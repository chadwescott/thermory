using System;
using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberSubCategory : ILumberSubCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ILumberCategory LumberCategory { get; set; }

        public int WidthInMillimeters { get; set; }

        public double WidthInInches { get { return LengthConverter.ConvertMillimetersToInches(WidthInMillimeters); } }

        public int ThicknessInMillimeters { get; set; }

        public double ThicknessInInches { get { return LengthConverter.ConvertMillimetersToInches(ThicknessInMillimeters); } }

        public IList<ILumberType> LumberTypes { get; set; }
    }
}
