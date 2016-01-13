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
        public int Width { get; set; }
        public int Thickness { get; set; }
    }
}
