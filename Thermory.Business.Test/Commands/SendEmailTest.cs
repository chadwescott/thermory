using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Business.Commands;

namespace Thermory.Business.Test.Commands
{
    [TestClass]
    public class SendEmailTest
    {
        [TestMethod]
        public void SendTest()
        {
            var command = new SendEmail();
            command.Execute();
        }
    }
}
