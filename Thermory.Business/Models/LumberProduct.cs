using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberProduct : Product<ILumberProductType, ILumberInventory, ILumberProduct>, ILumberProduct
    {
        public int LengthInMillmeters { get; set; }

        public double LengthInInches { get { return LengthConverter.ConvertMillimetersToInches(LengthInMillmeters); } }
    }
}
