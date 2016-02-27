using System;

namespace Thermory.Domain
{
    public interface IOrderLumberLineItem
    {
        Guid Id { get; }

        IOrder Order { get; }

        ILumberProduct LumberProduct { get; }

        int Quantity { get; }
    }
}
