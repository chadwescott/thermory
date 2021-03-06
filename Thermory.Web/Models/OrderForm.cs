﻿using System.Collections.Generic;
using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    public class OrderForm
    {
        public IList<Customer> Customers { get; set; }

        public IList<PackagingType> PackagingTypes { get; set; } 

        public Order Order { get; set; }

        public IList<LumberOrderForm> LumberOrderForms { get; set; }

        public IList<MiscellaneousOrderForm> MiscellaneousOrderForms { get; set; }
    }
}