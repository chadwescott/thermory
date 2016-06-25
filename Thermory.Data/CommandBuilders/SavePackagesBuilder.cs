using System;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class SavePackagesBuilder : CommandBuilder
    {
        public SavePackagesBuilder(int userId, Guid orderId, PackageLumberLineItem[] lineItems)
        {
            var order = DatabaseCommandDirectory.Instance.GetOrderById(orderId);
            Commands = !order.Packages.Any()
                ? new CreatePackagesBuilder(userId, order, lineItems).Commands
                : new EditPackagesBuilder(userId, order, lineItems).Commands;
        }
    }
}
