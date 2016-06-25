using Thermory.Data.Commands;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class CreatePackagesBuilder : OrderBuilder
    {
        public CreatePackagesBuilder(int userId, Order order, PackageLumberLineItem[] lineItems)
        {
            CreateInventoryTransaction(userId, order);
            CreatePackages(order, lineItems);
            order.OrderStatusId = DatabaseCommandDirectory.Instance.GetOrderStatusByEnum(OrderStatuses.PackagingSlipCreated).Id;
            Commands.Add(new SaveOrder(order));
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.PackagingSlipsCreated; }
        }
    }
}
