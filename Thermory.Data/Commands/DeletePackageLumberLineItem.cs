using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class DeletePackageLumberLineItem : DatabaseCommand
    {
        private readonly PackageLumberLineItem _lineItem;

        public DeletePackageLumberLineItem(PackageLumberLineItem lineItem)
        {
            _lineItem = lineItem;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.PackageLumberLineItems.Attach(_lineItem);
            context.PackageLumberLineItems.Remove(_lineItem);
            context.SaveChanges();
        }
    }
}
