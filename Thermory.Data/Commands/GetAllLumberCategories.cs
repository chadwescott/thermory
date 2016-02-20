using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberCategories : DatabaseGetCommand<IList<IDbLumberCategory>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.LumberCategories.Include(c => c.DbLumberSubCategories.Select(s => s.DbLumberTypes.Select(t => t.DbLumberProducts))).ToList<IDbLumberCategory>();
        }
    }
}
