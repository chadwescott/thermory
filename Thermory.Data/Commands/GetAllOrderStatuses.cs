using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllOrderStatuses : DatabaseGetCommand<IList<OrderStatus>>
    {
        private static List<OrderStatus> _result;

        protected override void OnExecute(ThermoryContext context)
        {
            if (_result == null)
                _result = context.OrderStatus.ToList();
            Result = _result;
        }
    }
}
