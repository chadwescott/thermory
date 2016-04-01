﻿using System;
using System.Data.Entity;
using Thermory.Domain.Models;

namespace Thermory.Data
{
    internal class ThermoryContext : DbContext
    {
        public ThermoryContext(string nameOrConnectionString = null, int minCommandTimeout = -1)
            : base(nameOrConnectionString)
        {
            Initialize(minCommandTimeout);
        }

        private void Initialize(int minCommandTimeout)
        {
            Database.CommandTimeout = Math.Max(Database.Connection.ConnectionTimeout, minCommandTimeout);
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }

        public DbSet<LumberProduct> LumberProducts { get; set; }

        public DbSet<LumberCategory> LumberCategories { get; set; }

        public DbSet<LumberSubCategory> LumberSubCategories { get; set; }

        public DbSet<LumberTransactionDetail> LumberTransactionDetails { get; set; }

        public DbSet<LumberType> LumberTypes { get; set; }

        public DbSet<MiscellaneousCategory> MiscellaneousCategories { get; set; }

        public DbSet<MiscellaneousSubCategory> MiscellaneousSubCategories { get; set; }

        public DbSet<MiscellaneousProduct> MiscellaneousProducts { get; set; }

        public DbSet<MiscellaneousTransactionDetail> MiscellaneousTransactionDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLumberLineItem> OrderLumberLineItems { get; set; }

        public DbSet<OrderMiscellaneousLineItem> OrderMiscellaneousLineItems { get; set; }

        public DbSet<OrderType> OrderTypes { get; set; }

        public DbSet<TransactionType> TransactionTypes { get; set; }
    }
}
