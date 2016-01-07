using System;

namespace Thermory.Domain
{
    public interface IProductFamily
    {
        Guid Id { get; }
        string Name { get; }
        bool IsActive { get; }
        Guid? ParentId { get; }
    }
}
