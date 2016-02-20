using System.Linq;
using Thermory.Domain;

namespace Thermory.Data.Commands
{
    internal class UpdateMiscellaneousProductInventory : DatabaseCommand
    {
        private readonly IMiscellaneousProduct _miscProduct;

        public UpdateMiscellaneousProductInventory(IMiscellaneousProduct miscProduct)
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
