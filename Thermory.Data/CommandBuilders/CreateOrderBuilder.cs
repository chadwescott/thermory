using System.Collections.Generic;
using Thermory.Data.Commands;
using Thermory.Data.Models;
using Thermory.Domain;
using Thermory.Domain.Enums;

namespace Thermory.Data.CommandBuilders
{
    internal class CreateOrderBuilder : ICommandBuilder
    {
        private readonly InventoryTransactionBuilder _inventoryTransactionBuilder;

        public CreateOrderBuilder(int userId, ILumberProduct[] lumberProducts,
            IMiscellaneousProduct[] miscProducts, Order order)
        {
            var transactionTypeId =
                DatabaseCommandDirectory.Instance.GetTransactionTypeIdByEnum(TransactionTypes.OrderCreate);
            var command = new CreateOrder(order);
            _inventoryTransactionBuilder = new InventoryTransactionBuilder(userId, transactionTypeId, lumberProducts,
                miscProducts);
            _inventoryTransactionBuilder.Commands.Add(command);
        }

        public List<DatabaseCommand> Commands
        {
            get { return _inventoryTransactionBuilder.Commands; }
        }
    }
}