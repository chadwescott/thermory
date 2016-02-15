using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class Inventory : IInventory<Product>
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}