using System;

namespace Thermory.Data.Models
{
    public interface IDbProductFamily
    {
        Guid Id { get; }
        string Name { get; }
        bool IsActive { get; }
        Guid? ParentId { get; }
    }
}
