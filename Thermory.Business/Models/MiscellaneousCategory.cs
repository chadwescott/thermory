using System;
using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class MiscellaneousCategory : IMiscellaneousCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<IMiscellaneousSubCategory> MiscellaneousSubCategories { get; set; }
    }
}
