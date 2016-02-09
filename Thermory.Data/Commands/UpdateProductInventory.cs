using System;
using System.Data.Entity;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class UpdateProductInventory : Command
    {
        private readonly ProductInventory _inventory;

        public UpdateProductInventory(Guid productId, int quantity)
        {
            _inventory = new ProductInventory
            {
                Id = productId,
                Quantity = quantity
            };
        }

        protected override void OnExecute()
        {
            InvokeRepository(c =>
            {
                c.ProductInventories.Attach(_inventory);
                foreach (var e in c.ChangeTracker.Entries())
                    e.State = EntityState.Modified;
                c.SaveChanges();
            });
        }
    }
}
