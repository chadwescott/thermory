using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetLumberTypeHistory : DatabaseGetCommand<IList<InventoryTransaction>>
    {
        private readonly Guid _lumberTypeId;

        public GetLumberTypeHistory(Guid lumberTypeId)
        {
            _lumberTypeId = lumberTypeId;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            Result =
                context.InventoryTransactions.Include(
                    c => c.LumberTransactionDetails.Where(s => s.LumberProduct.LumberTypeId == _lumberTypeId))
                    .OrderByDescending(i => i.CreatedOn)
                    .ToList();
        }
    }
}
