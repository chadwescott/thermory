using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllMiscellaneousCategories : DatabaseGetCommand<IList<IDbMiscellaneousCategory>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.MiscellaneousCategories.Include(c => c.DbMiscellaneousSubCategories.Select(s => s.DbMiscellaneousProducts)).ToList<IDbMiscellaneousCategory>();
        }
    }
}
