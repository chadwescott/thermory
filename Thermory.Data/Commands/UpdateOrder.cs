using System.Data.Entity;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class UpdateOrder : DatabaseCommand
    {
        private readonly Order _order;

        public UpdateOrder(Order order)
        {
            _order = order;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.Entry(_order).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
