using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    public class PackingSlip
    {
        public Package Package { get; set; }

        public int TotalPackages { get; set; }

        public string ShipDate
        {
            get
            {
                return Package.Order.ShipDate == null ? null : Package.Order.ShipDate.Value.ToString("MMMM dd, yyyy");
            }
        }

        private Customer _customer;
        public Customer Customer
        {
            get { return _customer ?? (_customer = Package.Order.Customer ?? new Customer()); }
        }

        public string Dimensions
        {
            get
            {
                return Package.Height == null || Package.Length == null || Package.Width == null
                    ? "" : string.Format("{0} x {1} x {2}", Package.Height, Package.Length, Package.Width);
            }
        }
    }
}