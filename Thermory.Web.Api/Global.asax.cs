using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Thermory.Web.Api.Constants;

namespace Thermory.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private delegate void InitializeRequest(HttpRequest request);
        private static InitializeRequest _initialize = InitializeBaseUrl;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.RegisterApiRouting);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        void Application_BeginRequest(Object source, EventArgs e)
        {
            _initialize(Request);
        }

        private static void InitializeBaseUrl(HttpRequest request)
        {
            _initialize = (r) => { };
            Routes.BaseUrl = GetBaseUrlFromConfig();
            if (string.IsNullOrEmpty(Routes.BaseUrl))
                Routes.BaseUrl = GetBaseUrlFromRequest(request);
        }

        private static string GetBaseUrlFromConfig()
        {
            const string BaseUrlKey = "BaseUrl";
            return ConfigurationManager.AppSettings.AllKeys.Contains(BaseUrlKey)
                ? ConfigurationManager.AppSettings[BaseUrlKey]
                : string.Empty;
        }

        private static string GetBaseUrlFromRequest(HttpRequest request)
        {
            var port = request.Url.Port == 80 || request.Url.Port == 443 ? string.Empty : $":{request.Url.Port}";
            return $"{request.Url.Scheme}://{request.Url.Host}{port}";
        }
    }
}
