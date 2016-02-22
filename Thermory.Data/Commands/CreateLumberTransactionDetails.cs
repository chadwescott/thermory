using System;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class CreateLumberTransactionDetails : DatabaseCommand
    {
        private readonly InventoryTransaction _transaction;
        private readonly Guid _lumberProductId;
        private readonly int _newQuantity;

        public CreateLumberTransactionDetails(InventoryTransaction transaction, Guid lumberProductId, int newQuantity)
        {
            _transaction = transaction;
            _lumberProductId = lumberProductId;
            _newQuantity = newQuantity;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            var lumberProduct = context.LumberProducts.Single(lp => lp.Id == _lumberProductId);
            var detail = new LumberTransactionDetail
            {
                InventoryTransactionId = _transaction.Id,
                LumberProductId = _lumberProductId,
                NewQuantity = _newQuantity,
                PreviousQuantity = lumberProduct.Quantity
            };
            context.LumberTransactionDetails.Add(detail);
            context.SaveChanges();
        }
    }
}
