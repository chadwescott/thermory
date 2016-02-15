using System;
using System.Collections.Generic;

namespace Thermory.Data.Models
{
    public interface IDbLumberCategory
    {
        Guid Id { get; }

        string Name { get; }

        int SortOrder { get; }

        IList<IDbLumberSubCategory> LumberSubCategories { get; }
    }
}
