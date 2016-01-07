using System.Collections.Generic;
using Thermory.Data;
using Thermory.Domain;

namespace Thermory.Business
{
    public class CommandDirectory
    {
        private static CommandDirectory _instance;

        public static CommandDirectory Instance
        {
            get { return _instance ?? (_instance = new CommandDirectory()); }
        }

        private CommandDirectory()
        { }

        public IEnumerable<IProductFamily> GetAllProductFamilies()
        {
            return DatabaseCommandDirectory.Instance.GetAllProductFamilies();
        }

        public IEnumerable<ILumberFamily> GetAllLumberFamilies()
        {
            return DatabaseCommandDirectory.Instance.GetAllLumberFamilies();
        }
    }
}
