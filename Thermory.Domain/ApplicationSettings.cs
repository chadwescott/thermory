using System.Configuration;

namespace Thermory.Domain
{
    public class ApplicationSettings
    {
        private static ApplicationSettings _instance;

        public static ApplicationSettings Instance
        {
            get { return _instance ?? (_instance = new ApplicationSettings()); }
        }

        private ApplicationSettings()
        {
            FromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"];
        }

        public string FromEmailAddress { get; private set; }
    }
}
