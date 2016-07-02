using System;
using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Data.Extensions;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal abstract class OrderBuilder : CommandBuilder
    {
        protected abstract TransactionTypes TransactionType { get; }

        protected Order GetOrder(Guid orderId)
        {
            var getOrderCommand = new GetOrderById(orderId);
            getOrderCommand.Execute();
            return getOrderCommand.Result;
        }

        protected void AddCreateOrderLumberLineItemCommands(Order order,
            IEnumerable<OrderLumberLineItem> updatedLumberLineItems)
        {
            var createOrderLumberLinesCommands = updatedLumberLineItems.MakeCreateOrderLumberLineItemCommands(order);
            Commands.AddRange(createOrderLumberLinesCommands);
        }

        protected void AddCreateOrderMiscellaneousLineItemCommands(Order order,
            IEnumerable<OrderMiscellaneousLineItem> updatedMiscellaneousLineItems)
        {
            var createOrderMiscellaneousLinesCommands =
                updatedMiscellaneousLineItems.MakeCreateOrderMiscellaneousLineItemCommands(order);
            Commands.AddRange(createOrderMiscellaneousLinesCommands);
        }

        protected InventoryTransaction CreateInventoryTransaction(int userId, Order order)
        {
            var transactionTypeId =
                DatabaseCommandDirectory.Instance.GetTransactionTypeIdByEnum(TransactionType);
            var transaction = new InventoryTransaction
            {
                UserId = userId,
                Order = order,
                OrderId = order.Id,
                TransactionTypeId = transactionTypeId
            };
            CreateInventoryTransaction(transaction);
            return transaction;
        }

        private void CreateInventoryTransaction(InventoryTransaction transaction)
        {
            var createInventoryTransactionCommand = new CreateInventoryTransaction(transaction);
            Commands.Add(createInventoryTransactionCommand);
        }

        protected void CreatePackages(Order order, PackageLumberLineItem[] lumberLineItems,
            PackageMiscellaneousLineItem[] miscLineItems)
        {
            var packages = ConsolidatePackages(lumberLineItems, miscLineItems);
            AddCreatePackageCommands(packages);
            AddCreatePackageLumberLineItemsCommands(packages, lumberLineItems);
            AddCreatePackageMiscellaneousLineItemsCommands(packages, miscLineItems);
        }

        private List<Package> ConsolidatePackages(IEnumerable<PackageLumberLineItem> lumberLineItems,
            IEnumerable<PackageMiscellaneousLineItem> miscLineItems)
        {
            var packages = lumberLineItems.Select(li => li.Package).ToList();
            packages.AddRange(miscLineItems.Select(li => li.Package).ToList());

            return
                packages.Select(p => p.PackageNumber)
                    .Distinct()
                    .Select(packageNumber => packages.First(p => p.PackageNumber == packageNumber))
                    .ToList();
        }

        private void AddCreatePackageCommands(IEnumerable<Package> packages)
        {
            foreach (var package in packages)
                Commands.Add(new CreatePackage(package));
        }

        private void AddCreatePackageLumberLineItemsCommands(List<Package> packages, IEnumerable<PackageLumberLineItem> lineItems)
        {
            foreach (var lineItem in lineItems.Where(li => li.Quantity > 0))
            {
                lineItem.Package = packages.Single(p => p.PackageNumber == lineItem.Package.PackageNumber);
                Commands.Add(new CreatePackageLumberLineItem(lineItem));
            }
        }

        private void AddCreatePackageMiscellaneousLineItemsCommands(List<Package> packages, IEnumerable<PackageMiscellaneousLineItem> lineItems)
        {
            foreach (var lineItem in lineItems.Where(li => li.Quantity > 0))
            {
                lineItem.Package = packages.Single(p => p.PackageNumber == lineItem.Package.PackageNumber);
                Commands.Add(new CreatePackageMiscellaneousLineItem(lineItem));
            }
        }
    }
}