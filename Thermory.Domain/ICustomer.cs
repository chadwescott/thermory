using System;

namespace Thermory.Domain
{
    public interface ICustomer
    {
        Guid Id { get; }

        string Name { get; }

        string AddressLine1 { get; }

        string AddressLine2 { get; }
    }
}
