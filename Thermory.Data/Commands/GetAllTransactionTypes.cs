using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllTransactionTypes : DatabaseGetCommand<IList<TransactionType>>
    {
        private static List<TransactionType> _result;

        protected override void OnExecute(ThermoryContext context)
        {
            if (_result == null)
                _result = context.TransactionTypes.ToList();
            Result = _result;
        }
    }
}
