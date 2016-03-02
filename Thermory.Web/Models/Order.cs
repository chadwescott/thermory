using System;
using Thermory.Domain;
using Thermory.Domain.Enums;

namespace Thermory.Web.Models
{
    public class Order : IOrder, IViewModel
    {
        public Guid Id { get; set; }

        public OrderTypes OrderType { get; set; }
        
        public int UserId { get; set; }

        public IOrderLumberLineItem[] LumberLineItems { get; set; }

        public IOrderMiscellaneousLineItem[] MiscellaneousLineItems { get; set; }

        public string recid
        {
            get { return Id.ToString(); }
            set { Id = new Guid(value); }
        }
    }
}