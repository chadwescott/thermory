using Thermory.Domain;

namespace Thermory.Business.Models
{

    internal class LumberInventory : ProductInventory<ILumberProductType>, ILumberInventory
    {
        public double LinearFeet { get; set; }
        public double SquareFeet { get; set; }
    }
}
