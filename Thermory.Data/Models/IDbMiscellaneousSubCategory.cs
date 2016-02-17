using System;
using System.Collections.Generic;

namespace Thermory.Data.Models
{
    public interface IDbMiscellaneousSubCategory
    {
        Guid Id { get; }

        Guid MiscellaneousCategoryId { get; }

        IDbMiscellaneousCategory MiscellaneousCategory { get; }

        string Name { get; }

        int SortOrder { get; }

        IList<IDbMiscellaneousProduct> MiscellaneousProducts { get; }
    }
}
