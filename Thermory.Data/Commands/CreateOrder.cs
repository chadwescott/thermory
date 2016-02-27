using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class CreateOrder : DatabaseCommand
    {
        private readonly Order _order;

        public CreateOrder(Order order)
        {
            _order = order;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.Orders.Add(_order);
            context.SaveChanges();
        }
    }
}
