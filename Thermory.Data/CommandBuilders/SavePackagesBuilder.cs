using System;
using Thermory.Data.Commands;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class SavePackagesBuilder : OrderBuilder
    {
        private readonly TransactionTypes _transactionType;

        public SavePackagesBuilder(int userId, Guid orderId, PackageLumberLineItem[] lineItems)
        {
            var order = GetOrder(orderId);
            _transactionType = order.Packages == null
                ? TransactionTypes.PackagingSlipsCreated
                : TransactionTypes.PackagingSlipsEdited;

            CreateInventoryTransaction(userId, order);

            order.OrderStatusId = DatabaseCommandDirectory.Instance.GetOrderStatusByEnum(OrderStatuses.PackagingSlipCreated).Id;
            Commands.Add(new SaveOrder(order));
        }

        protected override TransactionTypes TransactionType
        {
            get { return _transactionType; }
        }
    }
}
