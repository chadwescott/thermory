using System.Collections.Generic;
using Thermory.Data.Commands;
using Thermory.Domain;

namespace Thermory.Business.Commands
{
    internal class GetAllMiscellaneousProducts : IGetCommand<IList<IMiscellaneousProduct>>
    {
        public IList<IMiscellaneousProduct> Result { get; private set; }

        public void Execute()
        {
            Result = new List<IMiscellaneousProduct>();
        }
    }
}

