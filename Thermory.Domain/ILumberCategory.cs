using System;
using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface ILumberCategory
    {
        Guid Id { get; }

        string Name { get; }

        IList<ILumberSubCategory> LumberSubCategories { get; }
    }
}
