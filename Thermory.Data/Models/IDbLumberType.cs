using System;
using System.Collections.Generic;

namespace Thermory.Data.Models
{
    public interface IDbLumberType
    {
        Guid Id { get; }

        Guid LumberSubCategoryId { get; }

        IDbLumberSubCategory LumberSubCategory { get; }

        string Name { get; }

        int SortOrder { get; }

        IList<IDbLumberProduct> LumberProducts { get; }
    }
}
