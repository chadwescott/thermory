using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberProducts : GetCommand<IList<IDbLumberProduct>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.LumberProducts.ToList<IDbLumberProduct>();
        }
    }
}
