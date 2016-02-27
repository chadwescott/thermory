using System;
using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Data.Models;
using Thermory.Domain;

namespace Thermory.Data.CommandBuilders
{
    internal class InventoryTransactionBuilder : ICommandBuilder
    {
        public List<DatabaseCommand> Commands { get; private set; }

        public InventoryTransaction Transaction { get; private set; }

        public InventoryTransactionBuilder(int userId, Guid transactionTypeId, ILumberProduct[] lumberProducts,
            IMiscellaneousProduct[] miscProducts)
        {
            Commands = new List<DatabaseCommand>();

            Transaction = new InventoryTransaction {UserId = userId, TransactionTypeId = transactionTypeId};
            var createInventoryTransactionCommand = new CreateInventoryTransaction(Transaction);
            Commands.Add(createInventoryTransactionCommand);

            var createLumberTransactionDetailCommands =
                lumberProducts.Select(p => new CreateLumberTransactionDetails(Transaction, p.Id, p.Quantity));
            Commands.AddRange(createLumberTransactionDetailCommands);

            var createMiscTransactionDetailCommands =
                miscProducts.Select(p => new CreateMiscellaneousTransactionDetails(Transaction, p.Id, p.Quantity));
            Commands.AddRange(createMiscTransactionDetailCommands);

            var lumberUpdateCommands = lumberProducts.Select(lp => new UpdateLumberProductInventory(lp)).ToList();
            Commands.AddRange(lumberUpdateCommands);

            var miscUpdateCommands = miscProducts.Select(mp => new UpdateMiscellaneousProductInventory(mp)).ToList();
            Commands.AddRange(miscUpdateCommands);
        }
    }
}
