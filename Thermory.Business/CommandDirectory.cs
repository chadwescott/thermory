using System.Collections.Generic;
using System.Linq;
using Thermory.Business.Models;
using Thermory.Data;
using Thermory.Data.Models;
using Thermory.Domain;

namespace Thermory.Business
{
    public class CommandDirectory
    {
        private static CommandDirectory _instance;
        private static List<IProductCategory<ILumberSubCategory>> _lumberCategories;

        public static CommandDirectory Instance
        {
            get { return _instance ?? (_instance = new CommandDirectory()); }
        }

        private CommandDirectory()
        {
            if (_lumberCategories == null)
                InitializeLumberProducts();
        }

        private static void InitializeLumberProducts()
        {
            _lumberCategories = new List<IProductCategory<ILumberSubCategory>>();
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
                        Thickness = lumberSubFamily.Thickness,
                        Width = lumberSubFamily.Width,
                        ProductTypes = new List<ILumberProductType>()
                    };
                    var subFamily = lumberSubFamily;
                    foreach (var productType in productFamilies.Where(f => f.ParentId == subFamily.Id).OrderBy(f => f.SortOrder))
                    {
                        var subFamilyType = productType;
                        var type = new LumberProductType
                        {
                            Id = productType.Id,
                            Name = productType.Name,
                            SubCategory = subCategory,
                            Products = new List<ILumberProduct>()
                        };
                        subCategory.ProductTypes.Add(type);
                        foreach (var lumberProduct in lumberProducts.Where(p => p.LumberFamilyId == subFamilyType.Id).OrderBy(l => l.Length))
                        {
                            type.Products.Add(new LumberProduct
                            {
                                ProductType = type,
                                LengthInMillmeters = lumberProduct.Length
                            });
                        }
                    }
                    category.ProductSubCategories.Add(subCategory);
                }
                _lumberCategories.Add(category);
            }
        }

        public IList<IDbProductFamily> GetAllProductFamilies()
        {
            return DatabaseCommandDirectory.Instance.GetAllProductFamilies();
        }

        public IList<IProductCategory<ILumberSubCategory>> GetAllLumberProducts()
        {
            return _lumberCategories;
        }
    }
}
