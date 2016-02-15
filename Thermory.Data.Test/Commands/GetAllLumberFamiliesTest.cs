using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Data.Commands;

namespace Thermory.Data.Test.Commands
{
    [TestClass]
    public class GetAllLumberFamiliesTest
    {
        [TestMethod]
        public void ExecuteTest()
        {
            var command = new GetAllLumberFamilies();
            command.Execute();
            var actual = command.Result;
            Assert.AreEqual(29, actual.Count);
        }
    }
}
