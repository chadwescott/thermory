using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberProducts : DatabaseGetCommand<IList<IDbLumberProduct>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.Products.OfType<LumberProduct>().ToList<IDbLumberProduct>();
        }
    }
}
