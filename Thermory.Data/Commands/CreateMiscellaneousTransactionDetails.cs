using System;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class CreateMiscellaneousTransactionDetails : DatabaseContextCommand
    {
        private readonly InventoryTransaction _transaction;
        private readonly Guid _miscProductId;
        private readonly int _newQuantity;

        public CreateMiscellaneousTransactionDetails(InventoryTransaction transaction, Guid miscProductId, int newQuantity)
        {
            _transaction = transaction;
            _miscProductId = miscProductId;
            _newQuantity = newQuantity;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            var miscProduct = context.MiscellaneousProducts.Single(p => p.Id == _miscProductId);
            if (_newQuantity == miscProduct.Quantity)
                return;

            var detail = new MiscellaneousTransactionDetail
            {
                InventoryTransactionId = _transaction.Id,
                MiscellaneousProductId = _miscProductId,
                NewQuantity = _newQuantity,
                PreviousQuantity = miscProduct.Quantity
            };
            context.MiscellaneousTransactionDetails.Add(detail);
            context.SaveChanges();
        }
    }
}
