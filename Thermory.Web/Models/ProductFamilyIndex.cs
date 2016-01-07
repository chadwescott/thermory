using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class ProductFamilyIndex
    {
        public IProductFamily ActiveProductFamily { get; set; }
        public IEnumerable<IProductFamily> RootProductFamilies { get; set; }
    }
}