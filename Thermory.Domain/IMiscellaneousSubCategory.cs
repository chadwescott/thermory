using System;
using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface IMiscellaneousSubCategory
    {
        Guid Id { get; }

        IMiscellaneousCategory MiscellaneousCategory { get; }

        string Name { get; }

        IList<IMiscellaneousProduct> MiscellaneousProducts { get; }
    }
}
