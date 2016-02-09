using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberFamilies : GetCommand<IList<IDbLumberFamily>>
    {
        protected override void OnExecute()
        {
            InvokeRepository(c => Result = c.ProductFamilies.OfType<LumberFamily>().ToList<IDbLumberFamily>());
        }
    }
}
