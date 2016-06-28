using System;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class SavePackagesBuilder : CommandBuilder
    {
        public SavePackagesBuilder(int userId, Guid orderId, PackageLumberLineItem[] lumberLineItems, PackageMiscellaneousLineItem[] miscLineItems)
        {
            lumberLineItems = lumberLineItems ?? new PackageLumberLineItem[0];
            miscLineItems = miscLineItems ?? new PackageMiscellaneousLineItem[0];

            var order = DatabaseCommandDirectory.Instance.GetOrderById(orderId);
            ValidateAllProductsPackaged(order, lumberLineItems, miscLineItems);
            Commands = !order.Packages.Any()
                ? new CreatePackagesBuilder(userId, order, lumberLineItems, miscLineItems).Commands
                : new EditPackagesBuilder(userId, order, lumberLineItems, miscLineItems).Commands;
        }

        private void ValidateAllProductsPackaged(Order order, PackageLumberLineItem[] lumberLineItems, PackageMiscellaneousLineItem[] miscLineItems)
        {
            if (!AllProductsPackage(order, lumberLineItems, miscLineItems))
                throw new Exception("Not all products on the order have been packaged.");
        }

        private bool AllProductsPackage(Order order, PackageLumberLineItem[] lumberLineItems, PackageMiscellaneousLineItem[] miscLineItems)
        {
            return
                order.OrderLumberLineItems.All(
                    lumberLineItem =>
                        lumberLineItems.Where(li => li.LumberProductId == lumberLineItem.LumberProductId)
                            .Sum(li => li.Quantity) == lumberLineItem.Quantity) &&
                order.OrderMiscellaneousLineItems.All(
                    miscLineItem =>
                        miscLineItems.Where(li => li.MiscellaneousProductId == miscLineItem.MiscellaneousProductId)
                            .Sum(li => li.Quantity) == miscLineItem.Quantity);
        }
    }
}
