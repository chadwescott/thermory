using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberProductInventories : GetCommand<IList<IDbProductInventory>>
    {
        protected override void OnExecute()
        {
            InvokeRepository(c => Result = c.LumberProducts.Select(l => l.DbInventory).ToList<IDbProductInventory>());
        }
    }
}
