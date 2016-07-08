using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetAllLumberCategories : DatabaseGetCommand<IList<LumberCategory>>
    {
        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.LumberCategories.ToList();
            var subCategories = context.LumberSubCategories.ToList();
            var types = context.LumberTypes.ToList();
            var products = context.LumberProducts.ToList();

            Parallel.ForEach(types, (t) => t.LumberProducts = products.Where(p => p.LumberTypeId == t.Id).ToList());
            Parallel.ForEach(subCategories, (sc) => sc.LumberTypes = types.Where(t => t.LumberSubCategoryId == sc.Id).ToList());
            Parallel.ForEach(Result, (c) => c.LumberSubCategories = subCategories.Where(sc => sc.LumberCategoryId == c.Id).ToList());
        }
    }
}
