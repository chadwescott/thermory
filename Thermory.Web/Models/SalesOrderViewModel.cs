using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class SalesOrderViewModel : ICustomer
    {
        public string FirstName { get { return "John"; } }

        public string LastName { get { return "Smith"; } }

        public string StreetAddress { get { return "23 Main Street"; } }

        public string City { get { return "Batvia"; } }

        public string State { get { return "NY"; } }

        public string PostalCode { get { return "14020"; } }
    }
}
