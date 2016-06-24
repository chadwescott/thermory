using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class DeleteOrderPackageLumberLineItem : DatabaseCommand
    {
        private readonly PackageLumberLineItem _lineItem;

        public DeleteOrderPackageLumberLineItem(PackageLumberLineItem lineItem)
        {
            _lineItem = lineItem;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.OrderPackageLumberLineItems.Attach(_lineItem);
            context.OrderPackageLumberLineItems.Remove(_lineItem);
            context.SaveChanges();
        }
    }
}
