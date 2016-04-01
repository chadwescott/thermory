using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberProducts : DatabaseGetCommand<IList<LumberProduct>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.LumberProducts.ToList();
        }
    }
}
