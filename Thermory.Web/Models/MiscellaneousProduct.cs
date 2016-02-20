using System;
using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class MiscellaneousProduct : IMiscellaneousProduct
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int SortOrder { get; set; }

        public int Quantity { get; set; }

        public IMiscellaneousSubCategory MiscellaneousSubCategory { get; set; }
    }
}