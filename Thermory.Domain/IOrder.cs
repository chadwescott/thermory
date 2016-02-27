using System;
using Thermory.Domain.Enums;

namespace Thermory.Domain
{
    public interface IOrder
    {
        Guid Id { get; }

        OrderTypes OrderType { get; }

        string UserId { get; }

        DateTime CreatedOn { get; }

        ILumberProduct[] LumberProducts { get; }

        IMiscellaneousProduct[] MiscellaneousProducts { get; }
    }
}
