using System;
using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class LumberProduct : ILumberProduct
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public int LengthInMillmeters { get; set; }
        public double LengthInInches { get; set; }
        public double TallyPercentage { get; set; }
        public double LinearFeet { get; set; }
        public double SquareFeet { get; set; }
        public ILumberType LumberType { get; set; }
    }
}