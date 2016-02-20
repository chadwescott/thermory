using System;
using System.Collections.Generic;

namespace Thermory.Data.Models
{
    public interface IDbMiscellaneousCategory
    {
        Guid Id { get; }

        string Name { get; }

        int SortOrder { get; }

        IList<IDbMiscellaneousSubCategory> MiscellaneousSubCategories { get; }
    }
}
