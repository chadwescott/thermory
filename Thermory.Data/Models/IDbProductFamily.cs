using System;

namespace Thermory.Data.Models
{
    public interface IDbProductFamily
    {
        Guid Id { get; }

        Guid ProductTypeId { get; }

        Domain.ProductType ProductType { get; }

        string Name { get; }

        bool IsActive { get; }

        Guid? ParentId { get; }

        int SortOrder { get; }
    }
}
