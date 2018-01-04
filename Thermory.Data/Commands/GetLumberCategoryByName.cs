using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetLumberCategoryByName : DatabaseGetCommand<LumberCategory>
    {
        private readonly string _name;

        public GetLumberCategoryByName(string name)
        {
            _name = name;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.LumberCategories.SingleOrDefault(c => c.Name == _name);
        }
    }
}
