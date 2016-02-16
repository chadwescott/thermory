using System.Linq;
using Thermory.Domain;

namespace Thermory.Data.Commands
{
    internal class UpdateLumberProductInventory : DatabaseCommand
    {
        private readonly ILumberProduct _lumberProduct;

        public UpdateLumberProductInventory(ILumberProduct lumberProduct)
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
