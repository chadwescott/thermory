using System;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class AdjustLumberProductQuantity : DatabaseCommand
    {
        private readonly InventoryTransaction _transaction;

        private readonly Guid _lumberProductId;

        private readonly int _delta;

        public AdjustLumberProductQuantity(InventoryTransaction transaction, Guid lumberProductId, int delta)
        {
            _transaction = transaction;
            _lumberProductId = lumberProductId;
            _delta = delta;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            var lumberProduct = context.LumberProducts.Single(p => p.Id == _lumberProductId);
            var newQuantity = lumberProduct.Quantity + _delta;

            var command = new CreateLumberTransactionDetails(_transaction, _lumberProductId, newQuantity);
            command.Execute(context);

            lumberProduct.Quantity = newQuantity;
            context.SaveChanges();
        }
    }
}
