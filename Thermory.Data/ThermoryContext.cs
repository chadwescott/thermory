using System;
using System.Data.Entity;
using Thermory.Data.Models;

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

        public DbSet<ProductFamily> ProductFamilies { get; set; }

        public DbSet<LumberFamily> LumberFamilies { get; set; }
    }
}
