using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class UpdateMiscellaneousProductInventory : DatabaseCommand
    {
        private readonly MiscellaneousProduct _miscProduct;

        public UpdateMiscellaneousProductInventory(MiscellaneousProduct miscProduct)
        {
            _miscProduct = miscProduct;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            var dbMiscellaneousProduct = context.MiscellaneousProducts.SingleOrDefault(lp => lp.Id == _miscProduct.Id);
            if (dbMiscellaneousProduct == null)
                return;

            dbMiscellaneousProduct.Quantity = _miscProduct.Quantity;
            context.SaveChanges();
        }
    }
}
