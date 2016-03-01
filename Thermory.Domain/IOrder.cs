using System;
using Thermory.Domain.Enums;

namespace Thermory.Domain
{
    public interface IOrder
    {
        Guid Id { get; }

        OrderTypes OrderType { get; }

        int UserId { get; }

        IOrderLumberLineItem[] LumberLineItems{ get; }

        IOrderMiscellaneousLineItem[] MiscellaneousLineItems { get; }
    }
}
