using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberProductType : ILumberProductType
    {
        public int TotalPieces { get; private set; }
        public double TotalLinearFeet { get; private set; }
        public double TotalSquareFeet { get; private set; }
        public int[] LumberProductLengthsMillimeters { get; private set; }
        public double[] LumberProductLengthsFeet { get; private set; }
    }
}
