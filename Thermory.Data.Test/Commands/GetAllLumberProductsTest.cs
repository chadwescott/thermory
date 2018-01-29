using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Thermory.Data.Commands;

namespace Thermory.Data.Test.Commands
{
    [TestClass]
    public class GetAllLumberProductsTest
    {
        [TestMethod]
        public void ExecuteTest()
        {
            var command = new GetAllLumberProducts();
            command.Execute();
            var actual = command.Result;
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
        }
    }
}
