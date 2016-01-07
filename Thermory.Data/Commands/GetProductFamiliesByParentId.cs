using System;
using System.Collections.Generic;
using System.Linq;
using Thermory.Domain;

namespace Thermory.Data.Commands
{
    internal class GetProductFamiliesByParentId : GetCommand<IEnumerable<IProductFamily>>
    {
        private readonly Guid? _parentId;

        public GetProductFamiliesByParentId(Guid? parentId = null)
        {
            _parentId = parentId;
        }

        protected override void OnExecute()
        {
            InvokeRepositoryRead(c => Result = c.ProductFamilies.Where(pf => pf.ParentId == _parentId));
        }
    }
}
