using System;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class AdjustLumberProductQuantity : DatabaseCommand
    {
        private readonly InventoryTransaction _transaction;
        private readonly Guid _lumberProductId;
        private readonly int _delta;
        private readonly bool _applyQuantityChanges;

        public AdjustLumberProductQuantity(InventoryTransaction transaction, Guid lumberProductId, int delta, bool applyQuantityChanges)
        {
            _transaction = transaction;
            _lumberProductId = lumberProductId;
            _delta = delta;
            _applyQuantityChanges = applyQuantityChanges;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_delta == 0)
                return;

            var lumberProduct = context.LumberProducts.Single(p => p.Id == _lumberProductId);
            var newQuantity = lumberProduct.Quantity + _delta;

            if (_transaction != null)
            {
                var command = new CreateLumberTransactionDetails(_transaction, _lumberProductId, newQuantity);
                command.Execute(context);
            }

            if (!_applyQuantityChanges) return;

            lumberProduct.Quantity = newQuantity;
            context.SaveChanges();
        }
    }
}
