using System;

namespace Thermory.Domain
{
    public interface IMiscellaneousProduct
    {
        Guid Id { get; }

        string Name { get; }

        string Description { get; }

        int SortOrder { get; }

        int Quantity { get; set; }

        IMiscellaneousSubCategory MiscellaneousSubCategory { get; }
    }
}
