using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    public class PackingSlip
    {
        public Package Package { get; set; }

        public int TotalPackages { get; set; }
    }
}