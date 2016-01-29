using System;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberInventory : ProductInventory, ILumberInventory
    {
        public ILumberProduct Product { get; set; }

        public double TallyPercentage { get { return Math.Round(LinearFeet / Product.ProductType.TotalLinearFeet * 100, 0); } }

        public double LinearFeet { get { return Math.Round(Quantity*Product.LengthInInches/12, 0); } }

        public double SquareFeet
        {
            get { return Math.Round(LinearFeet*Product.ProductType.SubCategory.WidthInInches/12, 0); }
        }
    }
}
