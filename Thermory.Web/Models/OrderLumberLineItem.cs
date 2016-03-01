using System;
using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class OrderLumberLineItem : IOrderLumberLineItem
    {
        public Guid Id { get; set; }

        public IOrder Order { get; set; }

        public ILumberProduct LumberProduct { get; set; }

        public int Quantity { get; set; }
    }
}