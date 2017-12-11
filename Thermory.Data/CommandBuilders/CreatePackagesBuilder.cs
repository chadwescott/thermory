using Thermory.Data.Commands;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class CreatePackagesBuilder : OrderBuilder
    {
        public CreatePackagesBuilder(int userId, Order order, PackageLumberLineItem[] lumberLineItems, PackageMiscellaneousLineItem[] miscLineItems)
        {
            var transaction = MakeInventoryTransaction(userId, order);
            AddCreateInventoryTransactionCommand(transaction);
            CreatePackages(order, lumberLineItems, miscLineItems);
            order.OrderStatusId = DatabaseCommandDirectory.Instance.GetOrderStatusByEnum(OrderStatuses.PackingSlipCreated).Id;
            Commands.Add(new SaveOrder(order));
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.PackingSlipsCreated; }
        }
    }
}
