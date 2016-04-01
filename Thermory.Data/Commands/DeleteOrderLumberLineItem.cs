using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class DeleteOrderLumberLineItem : DatabaseCommand
    {
        private readonly OrderLumberLineItem _lineItem;

        public DeleteOrderLumberLineItem(OrderLumberLineItem lineItem)
        {
            _lineItem = lineItem;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            _lineItem.Order = null;
            _lineItem.LumberProduct = null;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.OrderLumberLineItems.Attach(_lineItem);
            context.OrderLumberLineItems.Remove(_lineItem);
            context.SaveChanges();
        }
    }
}
