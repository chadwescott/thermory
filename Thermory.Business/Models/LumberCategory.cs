using System;
using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberCategory : ILumberCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<ILumberSubCategory> LumberSubCategories { get; set; }
    }
}
