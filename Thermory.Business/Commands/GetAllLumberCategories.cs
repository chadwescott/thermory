using System.Collections.Generic;
using System.Linq;
using Thermory.Data;
using Thermory.Domain.Commands;
using Thermory.Domain.Models;

namespace Thermory.Business.Commands
{
    internal class GetAllLumberCategories : IGetCommand<IList<LumberCategory>>
    {
        public IList<LumberCategory> Result { get; private set; }

        public void Execute()
        {
            Result = DatabaseCommandDirectory.Instance.GetAllLumberCategories().OrderBy(c => c.SortOrder).ToList();
        }
    }
}
