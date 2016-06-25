using System;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class SavePackagesBuilder : CommandBuilder
    {
        public SavePackagesBuilder(int userId, Guid orderId, PackageLumberLineItem[] lumberLineItems, PackageMiscellaneousLineItem[] miscLineItems)
        {
            var order = DatabaseCommandDirectory.Instance.GetOrderById(orderId);
            Commands = !order.Packages.Any()
                ? new CreatePackagesBuilder(userId, order, lumberLineItems, miscLineItems).Commands
                : new EditPackagesBuilder(userId, order, lumberLineItems, miscLineItems).Commands;
        }
    }
}
