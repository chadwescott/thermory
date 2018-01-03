using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class DeleteOrderMiscellaneousLineItem : DatabaseContextCommand
    {
        private readonly OrderMiscellaneousLineItem _lineItem;

        public DeleteOrderMiscellaneousLineItem(OrderMiscellaneousLineItem lineItem)
        {
            _lineItem = lineItem;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            _lineItem.Order = null;
            _lineItem.MiscellaneousProduct = null;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.OrderMiscellaneousLineItems.Attach(_lineItem);
            context.OrderMiscellaneousLineItems.Remove(_lineItem);
            context.SaveChanges();
        }
    }
}
