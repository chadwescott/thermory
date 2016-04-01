using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllMiscellaneousProducts : DatabaseGetCommand<IList<MiscellaneousProduct>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.MiscellaneousProducts.ToList();
        }
    }
}
