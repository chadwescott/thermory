using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllCustomers : DatabaseGetCommand<IList<Customer>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.Customers.OrderBy(c => c.Name).ToList();
        }
    }
}
