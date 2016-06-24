using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class CreateOrderPackageLumberLineItem : DatabaseCommand
    {
        private readonly PackageLumberLineItem _lineItem;

        public CreateOrderPackageLumberLineItem(PackageLumberLineItem lineItem)
        {
            _lineItem = lineItem;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.OrderPackageLumberLineItems.Attach(_lineItem);
            context.SaveChanges();
        }
    }
}
