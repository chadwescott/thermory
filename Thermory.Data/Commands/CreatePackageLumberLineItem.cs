using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class CreatePackageLumberLineItem : DatabaseContextCommand
    {
        private readonly PackageLumberLineItem _lineItem;

        public CreatePackageLumberLineItem(PackageLumberLineItem lineItem)
        {
            _lineItem = lineItem;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            _lineItem.PackageId = _lineItem.Package.Id;
            base.OnBeforeExecute(context);
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.PackageLumberLineItems.Add(_lineItem);
            context.SaveChanges();
        }
    }
}
