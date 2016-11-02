using System.Collections.Generic;
using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    public class LumberCategoryViewModel
    {
        public LumberCategory Category { get; set; }

        public IList<LumberCategory> Categories { get; set; }
    }
}