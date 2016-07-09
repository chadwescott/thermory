using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberCategories : DatabaseGetCommand<IList<LumberCategory>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result =
                context.LumberCategories.Include(
                    c => c.LumberSubCategories.Select(s => s.LumberTypes.Select(t => t.LumberProducts)))
                    .ToList();
        }
    }
}
