using System.Collections.Generic;
using System.Linq;
using Thermory.Business.Models;
using Thermory.Data;
using Thermory.Data.Commands;
using Thermory.Domain;

namespace Thermory.Business.Commands
{
    internal class GetAllLumberProducts : IGetCommand<IList<IProductCategory<ILumberSubCategory>>>
    {
        public IList<IProductCategory<ILumberSubCategory>> Result { get; private set; }

        public void Execute()
        {
            Result = new List<IProductCategory<ILumberSubCategory>>();
            var productFamilies = DatabaseCommandDirectory.Instance.GetAllProductFamilies();
            var lumberFamilies = DatabaseCommandDirectory.Instance.GetAllLumberFamilies();
            var rootProductFamilies = productFamilies.Where(f => f.ParentId == null).ToList();
            var lumberProducts = DatabaseCommandDirectory.Instance.GetAllLumberProducts();

            foreach (var productFamily in rootProductFamilies.OrderBy(f => f.SortOrder))
            {
                var family = productFamily;
                var category = new ProductCategory<ILumberSubCategory>
                {
                    Id = productFamily.Id,
                    Name = productFamily.Name,
                    ProductSubCategories = new List<ILumberSubCategory>()
                };
                foreach (var lumberSubFamily in lumberFamilies.Where(f => f.ParentId == family.Id).OrderBy(f => f.SortOrder))
                {
                    var subCategory = new LumberSubCategory
                    {
                        Id = lumberSubFamily.Id,
                        Category = category,
                        Name = lumberSubFamily.Name,
                        ThicknessInMillimeters = lumberSubFamily.Thickness,
                        WidthInMillimeters = lumberSubFamily.Width,
                        ProductTypes = new List<ILumberProductType>()
                    };
                    var subFamily = lumberSubFamily;
                    foreach (var pt in productFamilies.Where(f => f.ParentId == subFamily.Id).OrderBy(f => f.SortOrder))
                    {
                        var productType = pt;
                        var type = new LumberProductType
                        {
                            Id = productType.Id,
                            Name = productType.Name,
                            SubCategory = subCategory,
                            Products = new List<ILumberProduct>()
                        };
                        subCategory.ProductTypes.Add(type);
                        foreach (var lumberProduct in lumberProducts.Where(p => p.ProductFamilyId == productType.Id).OrderBy(l => l.Length))
                        {
                            type.Products.Add(new LumberProduct
                            {
                                Id = lumberProduct.Id,
                                ProductType = type,
                                LengthInMillmeters = lumberProduct.Length
                            });
                        }
                    }
                    category.ProductSubCategories.Add(subCategory);
                }
                Result.Add(category);
            }
        }
    }
}
