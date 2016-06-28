using System.Collections.Generic;
using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    public class MiscellaneousOrderForm
    {
        public List<OrderMiscellaneousLineItem> MiscellaneousLineItems { get; set; }

        public MiscellaneousCategory MiscellaneousCategory { get; set; }

        public bool ValidateQuantityOnHand { get; set; }
    }
}