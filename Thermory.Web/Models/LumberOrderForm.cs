using System.Collections.Generic;
using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    public class LumberOrderForm
    {
        public List<OrderLumberLineItem> LumberLineItems { get; set; }

        public LumberCategory LumberCategory { get; set; }

        public bool ValidateQuantityOnHand { get; set; }
    }
}