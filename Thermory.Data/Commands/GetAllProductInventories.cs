using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllProductInventories : GetCommand<IList<IDbProductInventory>>
    {
        protected override void OnExecute()
        {
            InvokeRepositoryRead(c => Result = c.ProductInventories.ToList<IDbProductInventory>());
        }
    }
}
