using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class CreateInventoryTransaction : DatabaseCommand
    {
        private readonly InventoryTransaction _transaction;

        public CreateInventoryTransaction(InventoryTransaction transaction)
        {
            _transaction = transaction;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            if (_transaction.Order != null)
            {
                _transaction.OrderId = _transaction.Order.Id;
                _transaction.Order = null;
            }
            base.OnBeforeExecute(context);
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.InventoryTransactions.Add(_transaction);
            context.SaveChanges();
        }
    }
}
