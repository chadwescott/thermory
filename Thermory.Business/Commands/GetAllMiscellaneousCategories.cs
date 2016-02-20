using System.Collections.Generic;
using System.Linq;
using Thermory.Business.Models;
using Thermory.Data;
using Thermory.Data.Commands;
using Thermory.Domain;

namespace Thermory.Business.Commands
{
    internal class GetAllMiscellaneousCategories : IGetCommand<IList<IMiscellaneousCategory>>
    {
        public IList<IMiscellaneousCategory> Result { get; private set; }

        public void Execute()
        {
            Result = new List<IMiscellaneousCategory>();
            var dbMiscellaneousCategories = DatabaseCommandDirectory.Instance.GetAllMiscellaneousCategories().OrderBy(c => c.SortOrder);
            foreach (var dbMiscellaneousCategory in dbMiscellaneousCategories)
            {
                var miscellaneousCategory = new MiscellaneousCategory
                {
                    Id = dbMiscellaneousCategory.Id,
                    Name = dbMiscellaneousCategory.Name
                };

                var subCategories = new List<IMiscellaneousSubCategory>();
                foreach (var dbMiscellaneousSubCategory in dbMiscellaneousCategory.MiscellaneousSubCategories.OrderBy(sc => sc.SortOrder))
                {
                    var subCategory = new MiscellaneousSubCategory
                    {
                        Id = dbMiscellaneousSubCategory.Id,
                        Name = dbMiscellaneousSubCategory.Name,
                        MiscellaneousCategory = miscellaneousCategory
                    };

                    var miscellaneousProducts = dbMiscellaneousSubCategory.MiscellaneousProducts.OrderBy(p => p.SortOrder).Select(p => new MiscellaneousProduct
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        SortOrder = p.SortOrder,
                        Quantity = p.Quantity,
                        MiscellaneousSubCategory = subCategory
                    }).Cast<IMiscellaneousProduct>().ToList();

                    subCategory.MiscellaneousProducts = miscellaneousProducts;
                    subCategories.Add(subCategory);
                }

                miscellaneousCategory.MiscellaneousSubCategories = subCategories;
                Result.Add(miscellaneousCategory);
            }
        }
    }
}

