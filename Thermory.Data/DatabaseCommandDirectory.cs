using System;
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
            var command = new GetProductFamiliesByParentId();
            command.Execute();
            return command.Result;
        }

        public IEnumerable<IProductFamily> GetProductFamiliesByParentId(Guid parentId)
        {
            var command = new GetProductFamiliesByParentId(parentId);
            command.Execute();
            return command.Result;
        }

        public IEnumerable<IProductFamily> GetAllProductFamilies()
        {
            var command = new GetAllProductFamilies();
            command.Execute();
            return command.Result;
        }

        public IEnumerable<ILumberFamily> GetAllLumberFamilies()
        {
            var command = new GetAllLumberFamilies();
            command.Execute();
            return command.Result;
        }
    }
}
