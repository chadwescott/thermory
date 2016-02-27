using System;
using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class SalesOrderViewModel : ICustomer
    {
        public Guid Id { get; set; }

        public string Name { get { return "John Smith"; } }

        public string AddressLine1 { get { return "23 Main Street"; } }

        public string AddressLine2 { get { return "Batvia, NY 14020"; } }
    }
}
