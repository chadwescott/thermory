using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllRoles : DatabaseGetCommand<IList<WebPageRole>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.Roles.ToList();
        }
    }
}
