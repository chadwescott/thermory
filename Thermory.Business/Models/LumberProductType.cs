using System.Linq;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberProductType : ProductType<ILumberSubCategory, ILumberProductType, ILumberProduct>, ILumberProductType
    {
        public double TotalLinearFeet { get { return Products.Sum(p => p.Inventory.LinearFeet); } }

        public double TotalSquareFeet { get { return Products.Sum(p => p.Inventory.SquareFeet); } }

        public int[] LumberProductLengthsMillimeters { get { return Products.Select(p => p.LengthInMillmeters).ToArray(); } }

        public double[] LumberProductLengthsFeet { get { return Products.Select(p => LengthConverter.ConvertMillimetersToFeet(p.LengthInMillmeters)).ToArray(); } }
    }
}
