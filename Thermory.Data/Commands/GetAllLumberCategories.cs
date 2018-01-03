using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberCategories : DatabaseGetCommand<IList<LumberCategory>>
    {
        private readonly bool _shallow;

        public GetAllLumberCategories(bool shallow = false)
        {
            _shallow = shallow;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            Result =
                _shallow ? context.LumberCategories.ToList() :
                context.LumberCategories.Include(
                    c => c.LumberSubCategories.Select(s => s.LumberTypes.Select(t => t.LumberProducts)))
                    .ToList();
        }
    }
}
