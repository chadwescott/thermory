using System;
using System.Linq;
using Thermory.Data.Models;
using Thermory.Domain;

namespace Thermory.Data.Commands
{
    internal class GetLumberProductById : GetCommand<ILumberFamily>
    {
        private readonly Guid _id;

        public GetLumberProductById(Guid id)
        {
            _id = id;
        }

        protected override void OnExecute()
        {
            InvokeRepositoryRead(c => Result = c.ProductFamilies.OfType<LumberFamily>().SingleOrDefault(f => f.Id == _id));
        }
    }
}
