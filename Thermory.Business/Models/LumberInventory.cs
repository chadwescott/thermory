using System;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberInventory : ILumberInventory
    {
        //public double TallyPercentage { get { return Math.Round(LinearFeet / Product.ProductType.TotalLinearFeet * 100, 0); } }

        //public double LinearFeet { get { return Math.Round(Quantity*Product.LengthInInches/12, 0); } }

        //public double SquareFeet
        //{
        //    get { return Math.Round(LinearFeet*Product.ProductType.SubCategory.WidthInInches/12, 0); }
        //}
        public ILumberProduct Lumber { get; private set; }
        public int Quantity { get; set; }
        public double TallyPercentage { get; private set; }
        public double LinearFeet { get; private set; }
        public double SquareFeet { get; private set; }
    }
}
