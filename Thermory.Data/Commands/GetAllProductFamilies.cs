using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllProductFamilies : GetCommand<IList<IDbProductFamily>>
    {
        protected override void OnExecute()
        {
            InvokeRepository(c => Result = c.ProductFamilies.ToList<IDbProductFamily>());
        }
    }
}
