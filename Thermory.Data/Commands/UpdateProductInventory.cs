using System;
using System.Data.Entity;
using Thermory.Data.Models;
using Thermory.Domain;

namespace Thermory.Data.Commands
{
    internal class UpdateProductInventory<T> : DatabaseCommand
        where T : IProduct
    {
        private readonly IInventory<T>[] _inventory;

        public UpdateProductInventory(IInventory<T>[] inventory)
        {
            _inventory = inventory;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            foreach (var i in _inventory)
            {
                var dbInventory = new ProductInventory
                {
                    Id = i.Product.Id,
                    Quantity = i.Quantity
                };
                context.ProductInventories.Attach(dbInventory);
                foreach (var entry in context.ChangeTracker.Entries())
                    entry.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
