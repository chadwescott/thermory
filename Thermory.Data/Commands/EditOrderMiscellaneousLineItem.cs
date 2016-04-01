using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class EditOrderMiscellaneousLineItem : DatabaseCommand
    {
        private readonly OrderMiscellaneousLineItem _lineItem;

        public EditOrderMiscellaneousLineItem(OrderMiscellaneousLineItem lineItem)
        {
            _lineItem = lineItem;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.OrderMiscellaneousLineItems.Attach(_lineItem);
            context.SaveChanges();
        }
    }
}
