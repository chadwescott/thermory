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
            var command = new SendEmail("cwescott@gmail.com", "test@thermory.com", "Test Email", "This email was sent by a unit test.");
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
