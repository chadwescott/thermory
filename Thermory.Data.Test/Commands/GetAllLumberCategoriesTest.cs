using System;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Data.Commands;

namespace Thermory.Data.Test.Commands
{
    [TestClass]
    public class GetAllLumberCategoriesTest
    {
        [TestMethod]
        public void ExecuteByJoinTest()
        {
            XmlConfigurator.Configure();
            var command = new GetAllLumberCategories();
            command.Execute();
            var actual = command.Result;
            Assert.IsNotNull(actual);
        }
    }
}
