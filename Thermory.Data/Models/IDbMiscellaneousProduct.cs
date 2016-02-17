using System;

namespace Thermory.Data.Models
{
    public interface IDbMiscellaneousProduct
    {
        Guid Id { get; }

        Guid MiscellaneousSubCategoryId { get; }

        IDbMiscellaneousSubCategory MiscellaneousSubCategory { get; }

        string Name { get; }

        string Description { get; }

        int SortOrder { get; }

        int Quantity { get; }
    }
}
