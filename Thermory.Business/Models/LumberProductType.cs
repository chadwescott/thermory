using System.Linq;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberProductType : ILumberProductType
    {
        //public int TotalPieces { get { return Products.Sum(p => p.Inventory.Quantity); } }
        
        //public double TotalLinearFeet { get { return Products.Sum(p => p.Inventory.LinearFeet); } }

        //public double TotalSquareFeet { get { return Products.Sum(p => p.Inventory.SquareFeet); } }

        //public int[] LumberProductLengthsMillimeters { get { return Products.Select(p => p.LengthInMillmeters).ToArray(); } }

        //public double[] LumberProductLengthsFeet { get { return Products.Select(p => LengthConverter.ConvertMillimetersToFeet(p.LengthInMillmeters)).ToArray(); } }
        public int TotalPieces { get; private set; }
        public double TotalLinearFeet { get; private set; }
        public double TotalSquareFeet { get; private set; }
        public int[] LumberProductLengthsMillimeters { get; private set; }
        public double[] LumberProductLengthsFeet { get; private set; }
    }
}
