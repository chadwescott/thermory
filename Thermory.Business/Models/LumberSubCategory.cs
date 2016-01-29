using System;
using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberSubCategory : ILumberSubCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<ILumberProductType> ProductTypes { get; set; }
        public IProductCategory<ILumberSubCategory> Category { get; set; }
        public int WidthInMillimeters { get; set; }
        public double WidthInInches { get { return LengthConverter.ConvertMillimetersToInches(WidthInMillimeters); } }
        public int ThicknessInMillimeters { get; set; }
        public double ThicknessInInches { get { return LengthConverter.ConvertMillimetersToInches(ThicknessInMillimeters); } }
    }
}
