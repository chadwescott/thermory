using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Models;
using Thermory.Domain;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberFamilies : GetCommand<IEnumerable<ILumberFamily>>
    {
        protected override void OnExecute()
        {
            InvokeRepositoryRead(c => Result = c.ProductFamilies.OfType<LumberFamily>().ToList());
        }
    }
}
