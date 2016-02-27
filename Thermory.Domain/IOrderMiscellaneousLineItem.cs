using System;

namespace Thermory.Domain
{
    public interface IOrderMiscellaneousLineItem
    {
        Guid Id { get; }

        IOrder Order { get; }

        IMiscellaneousProduct MiscellaneousProduct { get; }

        int Quantity { get; }
    }
}
