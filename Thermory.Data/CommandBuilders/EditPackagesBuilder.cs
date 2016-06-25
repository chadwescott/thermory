using Thermory.Data.Commands;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class EditPackagesBuilder : OrderBuilder
    {
        public EditPackagesBuilder(int userId, Order order, PackageLumberLineItem[] lineItems)
        {
            CreateInventoryTransaction(userId, order);
            DeletePackages(order);
            CreatePackages(order, lineItems);
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.PackagingSlipsEdited; }
        }

        private void DeletePackages(Order order)
        {
            foreach (var package in order.Packages)
            {
                foreach (var lineItem in package.PackageLumberLineItems)
                    Commands.Add(new DeletePackageLumberLineItem(lineItem));
                Commands.Add(new DeletePackage(package));
            }
        }
    }
}
