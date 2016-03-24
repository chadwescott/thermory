using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllMiscellaneousCategories : DatabaseGetCommand<IList<MiscellaneousCategory>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result =
                context.MiscellaneousCategories.Include(
                    c => c.MiscellaneousSubCategories.Select(s => s.MiscellaneousProducts))
                    .ToList();
        }
    }
}
