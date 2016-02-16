using System;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberProduct : ILumberProduct
    {
        public Guid Id { get; set; }

        public int LengthInMillmeters { get; set; }

        public double LengthInInches { get { return LengthConverter.ConvertMillimetersToInches(LengthInMillmeters); } }

        public ILumberInventory Inventory { get; set; }
    }
}
