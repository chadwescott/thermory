using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetLumberTypeHistory : DatabaseGetCommand<IList<LumberTransactionDetail>>
    {
        private readonly Guid _lumberTypeId;

        public GetLumberTypeHistory(Guid lumberTypeId)
        {
            _lumberTypeId = lumberTypeId;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            Result =
                context.LumberTransactionDetails
                    .Include(d => d.InventoryTransaction.Order.OrderType)
                    .Include(d => d.InventoryTransaction.CreatedBy)
                    .Include(d => d.InventoryTransaction.TransactionType)
                    .Include(d => d.LumberProduct)
                    .Where(t => t.LumberProduct.LumberTypeId == _lumberTypeId)
                    .OrderByDescending(t => t.InventoryTransaction.CreatedOn)
                    .ToList();
        }
    }
}
