using System.Collections.Generic;
using System.Linq;
using Thermory.Data;
using Thermory.Domain.Commands;
using Thermory.Domain.Models;

namespace Thermory.Business.Commands
{
    internal class GetAllMiscellaneousCategories : IGetCommand<IList<MiscellaneousCategory>>
    {
        public IList<MiscellaneousCategory> Result { get; private set; }

        public void Execute()
        {
            Result = DatabaseCommandDirectory.Instance.GetAllMiscellaneousCategories().OrderBy(c => c.SortOrder).ToList();
        }
    }
}

