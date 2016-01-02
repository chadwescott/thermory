using System.Collections.Generic;
using System.Linq;
using Thermory.Domain;

namespace Thermory.Data.Commands
{
    internal class GetRootProductFamilies : GetCommand<IEnumerable<IProductFamily>>
    {
        protected override void OnExecute()
        {
            InvokeRepositoryRead(c => Results = c.ProductFamilies.Where(pf => pf.ParentId == null).ToList());
        }
    }
}
