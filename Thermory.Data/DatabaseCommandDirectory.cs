using System.Collections.Generic;
using Thermory.Data.Commands;
using Thermory.Domain;

namespace Thermory.Data
{
    public class DatabaseCommandDirectory
    {
        private static DatabaseCommandDirectory _instance;

        public static DatabaseCommandDirectory Instance
        {
            get { return _instance ?? (_instance = new DatabaseCommandDirectory()); }
        }

        private DatabaseCommandDirectory()
        { }

        public IEnumerable<IProductFamily> GetRootProductFamilies()
        {
            var command = new GetRootProductFamilies();
            command.Execute();
            return command.Results;
        }
    }
}
