using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Business.Commands;

namespace Thermory.Business.Test.Commands
{
    [TestClass]
    public class GetAllLumberProductsTest
    {
        [TestMethod]
        public void ExecuteTest()
        {
            var command = new GetAllLumberCategories();
            command.Execute();
            Assert.IsNotNull(command.Result);
        }
    }
}
