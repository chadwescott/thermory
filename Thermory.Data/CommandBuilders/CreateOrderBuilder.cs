using System.Linq;
using Thermory.Data.Commands;
using Thermory.Domain.Models;
using Thermory.Domain.Enums;
using Thermory.Domain.Utils;

namespace Thermory.Data.CommandBuilders
{
    internal class CreateOrderBuilder : OrderBuilder
    {
        public CreateOrderBuilder(int userId, string orderNumber, OrderTypes orderType, Customer customer, PackagingType packagingType,
            OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            var orderTypeId = DatabaseCommandDirectory.Instance.GetOrderTypeIdByEnum(orderType);

            if (customer.Name == null)
                customer = null;
            else
                Commands.Add(new SaveCustomer(customer));

            if (packagingType.Name == null)
                packagingType = null;
            else
                Commands.Add(new SavePackagingType(packagingType));

            Order = new Order { OrderTypeId = orderTypeId, OrderNumber = orderNumber, Customer = customer, PackagingType = packagingType};

            Commands.Add(new SaveOrder(Order));

            AddCreatedLumberLineItemCommands(Order, lumberLineItems);
            AddCreateMiscellaneousLineItemCommands(Order, miscLineItems);

            var transaction = CreateInventoryTransaction(userId, Order);

            var adjustmentMultiplier = AdjustmentMultiplier.GetByOrderType(orderType);

            var adjustLumberProductQuantityCommands =
                lumberLineItems.Select(i => new AdjustLumberProductQuantity(transaction, i.LumberProductId, i.Quantity * adjustmentMultiplier));
            Commands.AddRange(adjustLumberProductQuantityCommands);

            var adjustMiscellaneousProductQuantityCommands =
                miscLineItems.Select(i => new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId, i.Quantity * adjustmentMultiplier));
            Commands.AddRange(adjustMiscellaneousProductQuantityCommands);
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderCreate; }
        }
    }
}