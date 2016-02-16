using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllMiscellaneousProducts : DatabaseGetCommand<IList<IDbMiscellaneousProduct>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.MiscellaneousProducts.ToList<IDbMiscellaneousProduct>();
        }
    }
}
