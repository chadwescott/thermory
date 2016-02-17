using System.Collections.Generic;
using System.Linq;
using Thermory.Business.Models;
using Thermory.Data;
using Thermory.Data.Commands;
using Thermory.Domain;

namespace Thermory.Business.Commands
{
    internal class GetAllLumberCategories : IGetCommand<IList<ILumberCategory>>
    {
        public IList<ILumberCategory> Result { get; private set; }

        public void Execute()
        {
            Result = new List<ILumberCategory>();
            var dbLumberCategories = DatabaseCommandDirectory.Instance.GetAllLumberCategories().OrderBy(c => c.SortOrder);
            foreach (var dbLumberCategory in dbLumberCategories)
            {
                var lumberCategory = new LumberCategory
                {
                    Id = dbLumberCategory.Id,
                    Name = dbLumberCategory.Name
                };

                var subCategories = new List<ILumberSubCategory>();
                foreach (var dbLumberSubCategory in dbLumberCategory.LumberSubCategories.OrderBy(sc => sc.SortOrder))
                {
                    var subCategory = new LumberSubCategory
                    {
                        Id = dbLumberSubCategory.Id,
                        Name = dbLumberSubCategory.Name,
                        ThicknessInMillimeters = dbLumberSubCategory.Thickness,
                        WidthInMillimeters = dbLumberSubCategory.Width,
                        LumberCategory = lumberCategory
                    };

                    var lumberTypes = new List<ILumberType>();
                    foreach (var dbLumberType in dbLumberSubCategory.LumberTypes.OrderBy(lt => lt.SortOrder))
                    {
                        var lumberType = new LumberType
                        {
                            Id = dbLumberType.Id,
                            Name = dbLumberType.Name,
                            SortOrder = dbLumberType.SortOrder,
                            LumberSubCategory = subCategory
                        };

                        var lumberProducts = dbLumberType.LumberProducts.OrderBy(lp => lp.Length).Select(lp => new LumberProduct
                        {
                            Id = lp.Id,
                            LengthInMillmeters = lp.Length,
                            Quantity = lp.Quantity,
                            LumberType = lumberType
                        }).Cast<ILumberProduct>().ToList();

                        lumberType.LumberProducts = lumberProducts;
                        lumberTypes.Add(lumberType);
                    }

                    subCategory.LumberTypes = lumberTypes;
                    subCategories.Add(subCategory);
                }
                
                lumberCategory.LumberSubCategories = subCategories;
                Result.Add(lumberCategory);
            }
        }
    }
}
