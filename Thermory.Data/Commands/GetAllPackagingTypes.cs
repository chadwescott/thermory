using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllPackagingTypes : DatabaseGetCommand<IList<PackagingType>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.PackagingTypes.OrderBy(c => c.Name).ToList();
        }
    }
}
