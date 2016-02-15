using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Data.Commands;

namespace Thermory.Data.Test.Commands
{
    [TestClass]
    public class GetAllMiscellaneousProductsTest
    {
        [TestMethod]
        public void ExecuteTest()
        {
            var command = new GetAllMiscellaneousProducts();
            command.Execute();
            var actual = command.Result;
            Assert.AreEqual(0, actual.Count);
        }
    }
}
