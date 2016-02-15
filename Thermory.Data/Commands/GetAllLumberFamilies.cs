using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberFamilies : DatabaseGetCommand<IList<IDbLumberFamily>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.ProductFamilies.OfType<LumberFamily>().ToList<IDbLumberFamily>();
        }
    }
}
