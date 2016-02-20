using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Data.Commands;

namespace Thermory.Data.Test.Commands
{
    [TestClass]
    public class GetAllMiscellaneousCategoriesTest
    {
        [TestMethod]
        public void ExecuteTest()
        {
            var command = new GetAllMiscellaneousCategories();
            command.Execute();
            var actual = command.Result;
            Assert.AreEqual(1, actual.Count);
        }
    }
}
