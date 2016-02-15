using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberProductInventories : DatabaseGetCommand<IList<IDbProductInventory>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.LumberProducts.Select(l => l.DbInventory).ToList<IDbProductInventory>();
        }
    }
}
