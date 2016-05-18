using System;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class AdjustMiscellaneousProductQuantity : DatabaseCommand
    {
        private readonly InventoryTransaction _transaction;
        private readonly Guid _miscellaneousProductId;
        private readonly int _delta;
        private readonly bool _applyQuantityChanges;

        public AdjustMiscellaneousProductQuantity(InventoryTransaction transaction, Guid miscellaneousProductId, int delta, bool applyQuantityChanges)
        {
            _transaction = transaction;
            _miscellaneousProductId = miscellaneousProductId;
            _delta = delta;
            _applyQuantityChanges = applyQuantityChanges;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_delta == 0)
                return;

            var miscellaneousProduct = context.MiscellaneousProducts.Single(p => p.Id == _miscellaneousProductId);
            var newQuantity = miscellaneousProduct.Quantity + _delta;

            var command = new CreateMiscellaneousTransactionDetails(_transaction, _miscellaneousProductId, newQuantity);
            command.Execute(context);
            
            if (!_applyQuantityChanges) return;

            miscellaneousProduct.Quantity = newQuantity;
            context.SaveChanges();
        }
    }
}
