using System;
using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberType : ILumberType
    {
        public Guid Id { get; set; }

        public ILumberSubCategory LumberSubCategory { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public IList<ILumberProduct> LumberProducts { get; set; }
    }
}
