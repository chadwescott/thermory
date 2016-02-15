using System.Collections.Generic;
using System.Linq;
using Thermory.Business.Models;
using Thermory.Data;
using Thermory.Data.Commands;
using Thermory.Domain;
using ProductType = Thermory.Domain.ProductType;

namespace Thermory.Business.Commands
{
    internal class GetAllMiscellaneousProducts : IGetCommand<IList<IMiscellaneousProduct>>
    {
        public IList<IMiscellaneousProduct> Result { get; private set; }

        public void Execute()
        {
            Result = new List<IMiscellaneousProduct>();
            var productFamilies = DatabaseCommandDirectory.Instance.GetAllProductFamilies();
            var rootProductFamilies = productFamilies.Where(f => f.ParentId == null && f.ProductType == ProductType.Misc).ToList();
            var products = DatabaseCommandDirectory.Instance.GetAllMiscellaneousProducts();

        //    foreach (var productFamily in rootProductFamilies.OrderBy(f => f.SortOrder))
        //    {
        //        var family = productFamily;
        //        var category = new ProductCategory<IProductSubCategory>
        //        {
        //            Id = productFamily.Id,
        //            Name = productFamily.Name,
        //            ProductSubCategories = new List<IProductSubCategory>()
        //        };

        //        foreach (var lumberSubFamily in productFamilies.Where(f => f.ParentId == family.Id).OrderBy(f => f.SortOrder))
        //        {
        //            var subCategory = new ProductSubCategory
        //            {
        //                Id = lumberSubFamily.Id,
        //                Category = new ProductCategory<ProductSubCategory>
        //                {
        //                    Id = category.Id,
        //                    Name = category.Name
        //                },
        //                Name = lumberSubFamily.Name
        //            };
        //            var subFamily = lumberSubFamily;


        //        foreach (var product in products.Where(p => p.ProductFamilyId == family.Id))
        //        {
        //            subFamily.Products.Add(new LumberProduct
        //            {
        //                Id = product.Id,
        //                ProductType = type,
        //                LengthInMillmeters = product.Length
        //            });
        //        }
        //        //foreach (var lumberSubFamily in lumberFamilies.Where(f => f.ParentId == family.Id).OrderBy(f => f.SortOrder))
        //        //{
        //        //    var subCategory = new ProductSubCategory
        //        //    {
        //        //        Id = lumberSubFamily.Id,
        //        //        Category = new ProductCategory<ProductSubCategory>
        //        //        {
        //        //            Id = category.Id,
        //        //            Name = category.Name
        //        //        },
        //        //        Name = lumberSubFamily.Name
        //        //    };
        //        //    var subFamily = lumberSubFamily;
        //        //    foreach (var pt in productFamilies.Where(f => f.ParentId == subFamily.Id).OrderBy(f => f.SortOrder))
        //        //    {
        //        //        var productType = pt;
        //        //        var type = new LumberProductType
        //        //        {
        //        //            Id = productType.Id,
        //        //            Name = productType.Name,
        //        //            SubCategory = new ProductSubCategory
        //        //            {
        //        //                Id = subCategory.Id,
        //        //                Name = subCategory.Name
        //        //            },
        //        //            Products = new List<IProduct>()
        //        //        };
        //        //        subCategory.ProductTypes.Add(type);
        //        //        foreach (var lumberProduct in lumberProducts.Where(p => p.ProductFamilyId == productType.Id).OrderBy(l => l.Length))
        //        //        {
        //        //            type.Products.Add(new LumberProduct
        //        //            {
        //        //                Id = lumberProduct.Id,
        //        //                ProductType = type,
        //        //                LengthInMillmeters = lumberProduct.Length
        //        //            });
        //        //        }
        //        //    }
        //        //    category.ProductSubCategories.Add(subCategory);
        //        //}
        //        //Result.Add(category);
        //    }
        }
    }
}

