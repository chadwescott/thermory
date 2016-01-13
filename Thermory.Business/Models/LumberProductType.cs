using System.Linq;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberProductType : ProductType<ILumberSubCategory, ILumberProduct>, ILumberProductType
    {
        public int[] LumberProductLengthsMillimeters { get { return Products.Select(p => p.LengthInMillmeters).ToArray(); } }
        public double[] LumberProductLengthsFeet { get { return Products.Select(p => LengthConverter.ConvertMillimetersToFeet(p.LengthInMillmeters)).ToArray(); } }
    }
}
