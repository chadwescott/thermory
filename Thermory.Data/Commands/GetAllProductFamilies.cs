using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllProductFamilies : DatabaseGetCommand<IList<IDbProductFamily>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.ProductFamilies.Include("DbProductType").ToList<IDbProductFamily>();
        }
    }
}
