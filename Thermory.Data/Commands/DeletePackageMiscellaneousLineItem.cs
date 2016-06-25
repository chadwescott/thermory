using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class DeletePackageMiscellaneousLineItem : DatabaseCommand
    {
        private readonly PackageMiscellaneousLineItem _lineItem;

        public DeletePackageMiscellaneousLineItem(PackageMiscellaneousLineItem lineItem)
        {
            _lineItem = lineItem;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.PackageMiscellaneousLineItems.Attach(_lineItem);
            context.PackageMiscellaneousLineItems.Remove(_lineItem);
            context.SaveChanges();
        }
    }
}
