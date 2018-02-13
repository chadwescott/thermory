using Thermory.Domain.Models;
using Thermory.Web.Api.Constants;

namespace Thermory.Web.Api.Routing
{
    public class LumberCategoryRoute : Route
    {
        protected LumberCategoryRoute(string path)
            : base(path)
        { }

        protected LumberCategoryRoute(Route route, string path)
            : base(route, path)
        { }

        public static Route GetLumberCategories(Versions version)
        {
            return new LumberCategoryRoute(MakePath(version, Routes.LumberCategories));
        }

        public static Route GetLumberCategorysById(Versions version, LumberCategory category)
        {
            return new LumberCategoryRoute(GetLumberCategories(version), category.Id.ToString());
        }
    }
}