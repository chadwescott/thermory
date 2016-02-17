using System;
using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface IMiscellaneousCategory
    {
        Guid Id { get; }

        string Name { get; }

        IList<IMiscellaneousSubCategory> MiscellaneousSubCategories { get; }
    }
}
