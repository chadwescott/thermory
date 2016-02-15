using System;
using System.Collections.Generic;

namespace Thermory.Data.Models
{
    public interface IDbLumberSubCategory
    {
        Guid Id { get; }

        Guid LumberCategoryId { get; }

        IDbLumberCategory LumberCategory { get; } 

        string Name { get; }

        int Width { get; }

        int Thickness { get; }

        int SortOrder { get; }

        IList<IDbLumberType> LumberTypes { get; }
    }
}
