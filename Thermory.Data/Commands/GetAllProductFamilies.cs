using System.Collections.Generic;
using System.Linq;
using Thermory.Domain;

namespace Thermory.Data.Commands
{
    internal class GetAllProductFamilies : GetCommand<IEnumerable<IProductFamily>>
    {
        protected override void OnExecute()
        {
            InvokeRepositoryRead(c => Result = c.ProductFamilies.ToList());
        }
    }
}
