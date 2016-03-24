using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class EditOrderLumberLineItem : DatabaseCommand
    {
        private readonly OrderLumberLineItem _lineItem;

        public EditOrderLumberLineItem(OrderLumberLineItem lineItem)
        {
            _lineItem = lineItem;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.OrderLumberLineItems.Attach(_lineItem);
            context.SaveChanges();
        }
    }
}
