using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class InventoryWorksheet
    {
        public IEnumerable<IProductFamily> ProductFamilies { get; set; }
        public IEnumerable<ILumberFamily> LumberFamilies { get; set; }
    }
}