using System.Data.Entity;
using System;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetPackageById : DatabaseGetCommand<Package>
    {
        private readonly Guid _id;

        public GetPackageById(Guid id)
        {
            _id = id;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            Result =
                context.Packages
                    .Include(p => p.Order.PackagingType)
                    .Include(p => p.Order.Customer)
                    .Include(
                        p =>
                            p.PackageLumberLineItems.Select(
                                li => li.LumberProduct.LumberType.LumberSubCategory.LumberCategory))
                    .Include(
                        p =>
                            p.PackageMiscellaneousLineItems.Select(
                                li => li.MiscellaneousProduct.MiscellaneousSubCategory))
                    .SingleOrDefault(p => p.Id == _id);
        }
    }
}
