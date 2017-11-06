using System;
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
            var command = new SendEmail("no-reply@thermoryinventory.com", "chadwescott@gmail.com", "test", "This is a test email");
            try
            {
                command.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
