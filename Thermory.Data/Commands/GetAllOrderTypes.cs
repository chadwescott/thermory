using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllOrderTypes : DatabaseGetCommand<IList<OrderType>>
    {
        private static List<OrderType> _result;

        protected override void OnExecute(ThermoryContext context)
        {
            if (_result == null)
                _result = context.OrderTypes.ToList();
            Result = _result;
        }
    }
}
