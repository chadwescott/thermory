namespace Thermory.Web.Api.Constants
{
    public class Versions
    {
        public static class Names
        {
            public const string V1 = "v1";
        }

        public string Name { get; private set; }

        private Versions()
        { }

        public static Versions V1 { get; } = new Versions { Name = Names.V1 };
    }
}