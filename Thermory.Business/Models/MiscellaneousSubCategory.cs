using System;
using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class MiscellaneousSubCategory : IMiscellaneousSubCategory
    {
        public Guid Id { get; set; }

        public IMiscellaneousCategory MiscellaneousCategory { get; set; }

        public string Name { get; set; }

        public IList<IMiscellaneousProduct> MiscellaneousProducts { get; set; }
    }
}
