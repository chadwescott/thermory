using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberSubCategory : ILumberSubCategory
    {
        public string Name { get; set; }
        public IList<IProductType<ILumberSubCategory>> ProductTypes { get; set; }
        public IProductCategory<ILumberSubCategory> Category { get; set; }
        public int Width { get; set; }
        public int Thickness { get; set; }
    }
}
