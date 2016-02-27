using System;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Data.Models;
using Thermory.Domain;

namespace Thermory.Data.CommandBuilders
{
    internal class InventoryAuditBuilder : CommandBuilder
    {
        public InventoryAuditBuilder(int userId, Guid transactionTypeId, ILumberProduct[] lumberProducts,
            IMiscellaneousProduct[] miscProducts)
        {
            var transaction = new InventoryTransaction {UserId = userId, TransactionTypeId = transactionTypeId};
            var createInventoryTransactionCommand = new CreateInventoryTransaction(transaction);
            Commands.Add(createInventoryTransactionCommand);

            var createLumberTransactionDetailCommands =
                lumberProducts.Select(p => new CreateLumberTransactionDetails(transaction, p.Id, p.Quantity));
            Commands.AddRange(createLumberTransactionDetailCommands);

            var createMiscTransactionDetailCommands =
                miscProducts.Select(p => new CreateMiscellaneousTransactionDetails(transaction, p.Id, p.Quantity));
            Commands.AddRange(createMiscTransactionDetailCommands);

            var lumberUpdateCommands = lumberProducts.Select(lp => new UpdateLumberProductInventory(lp)).ToList();
            Commands.AddRange(lumberUpdateCommands);

            var miscUpdateCommands = miscProducts.Select(mp => new UpdateMiscellaneousProductInventory(mp)).ToList();
            Commands.AddRange(miscUpdateCommands);
        }
    }
}
