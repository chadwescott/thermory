using System;

namespace Thermory.Web.Models
{
    public class ProductOrderQuantity
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}