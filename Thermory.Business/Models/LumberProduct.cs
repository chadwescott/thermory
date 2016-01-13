using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberProduct : Product<ILumberProductType>, ILumberProduct
    {
        public int LengthInMillmeters { get; set; }
        public double LengthInInches { get { return LengthConverter.ConvertMillimetersToInches(LengthInMillmeters); } }
    }
}
