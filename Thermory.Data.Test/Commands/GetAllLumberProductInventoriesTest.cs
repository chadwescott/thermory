using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Data.Commands;

namespace Thermory.Data.Test.Commands
{
    [TestClass]
    public class GetAllLumberProductInventoriesTest
    {
        [TestMethod]
        public void ExecuteTest()
        {
            var command = new GetAllLumberProductInventories();
            command.Execute();
            var actual = command.Result;
            Assert.AreEqual(435, actual.Count);
        }
    }
}
