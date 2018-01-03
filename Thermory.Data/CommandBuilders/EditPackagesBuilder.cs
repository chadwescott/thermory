using Thermory.Data.Commands;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class EditPackagesBuilder : OrderBuilder
    {
        public EditPackagesBuilder(int userId, Order order, PackageLumberLineItem[] lumberLineItems, PackageMiscellaneousLineItem[] miscLineItems)
        {
            var transaction = MakeInventoryTransaction(userId, order);
            AddCreateInventoryTransactionCommand(transaction);
            DeletePackages(order);
            CreatePackages(order, lumberLineItems, miscLineItems);
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.PackingSlipsEdited; }
        }

        private void DeletePackages(Order order)
        {
            foreach (var package in order.Packages)
            {
                foreach (var lineItem in package.PackageLumberLineItems)
                    Commands.Add(new DeletePackageLumberLineItem(lineItem));
                foreach (var lineItem in package.PackageMiscellaneousLineItems)
                    Commands.Add(new DeletePackageMiscellaneousLineItem(lineItem));
                Commands.Add(new DeletePackage(package));
            }
        }
    }
}
