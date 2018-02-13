using Thermory.Web.Api.Constants;

namespace Thermory.Web.Api.Routing
{
    public abstract class Route
    {
        public string Path { get; private set; }

        protected Route(string path) { Path = path; }

        protected Route(Route route, string path)
            : this($"{route.Path}/{path}")
        { }

        protected static string MakePath(Versions version, string path)
        {
            return $"{Routes.BaseUrl}/{Routes.Api}/{version.Name}/{path}";
        }
    }
}