using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class CreateInventoryTransaction : DatabaseCommand
    {
        private readonly InventoryTransaction _transaction;

        public CreateInventoryTransaction(InventoryTransaction transaction)
        {
            _transaction = transaction;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.InventoryTransactions.Add(_transaction);
            context.SaveChanges();
        }
    }
}
