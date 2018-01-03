using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class UpdateLumberProductInventory : DatabaseContextCommand
    {
        private readonly LumberProduct _lumberProduct;

        public UpdateLumberProductInventory(LumberProduct lumberProduct)
        {
            _lumberProduct = lumberProduct;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            var dbLumberProduct = context.LumberProducts.SingleOrDefault(lp => lp.Id == _lumberProduct.Id);
            if (dbLumberProduct == null)
                return;

            dbLumberProduct.Quantity = _lumberProduct.Quantity;
            context.SaveChanges();
        }
    }
}
