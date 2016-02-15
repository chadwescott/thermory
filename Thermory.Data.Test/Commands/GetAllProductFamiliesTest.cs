using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Data.Commands;

namespace Thermory.Data.Test.Commands
{
    [TestClass]
    public class GetAllProductFamiliesTest
    {
        [TestMethod]
        public void ExecuteTest()
        {
            var command = new GetAllProductFamilies();
            command.Execute();
            var actual = command.Result;
            Assert.AreEqual(77, actual.Count);
        }
    }
}
