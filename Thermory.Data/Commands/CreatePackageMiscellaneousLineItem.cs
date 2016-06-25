using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class CreatePackageMiscellaneousLineItem : DatabaseCommand
    {
        private readonly PackageMiscellaneousLineItem _lineItem;

        public CreatePackageMiscellaneousLineItem(PackageMiscellaneousLineItem lineItem)
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
            context.PackageMiscellaneousLineItems.Add(_lineItem);
            context.SaveChanges();
        }
    }
}
