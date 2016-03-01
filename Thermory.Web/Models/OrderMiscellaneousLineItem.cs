using System;
using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class OrderMiscellaneousLineItem : IOrderMiscellaneousLineItem
    {
        public Guid Id { get; set; }

        public IOrder Order { get; set; }

        public IMiscellaneousProduct MiscellaneousProduct { get; set; }

        public int Quantity { get; set; }
    }
}