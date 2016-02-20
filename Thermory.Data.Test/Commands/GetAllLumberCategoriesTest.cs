using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Data.Commands;

namespace Thermory.Data.Test.Commands
{
    [TestClass]
    public class GetAllLumberCategoriesTest
    {
        [TestMethod]
        public void ExecuteTest()
        {
            var command = new GetAllLumberCategories();
            command.Execute();
            var actual = command.Result;
            Assert.AreEqual(8, actual.Count);
        }
    }
}
