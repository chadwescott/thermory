using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Data.Commands;

namespace Thermory.Data.Test.Commands
{
    [TestClass]
    public class GetLumberTypeHistoryTest
    {
        [TestMethod]
        public void ValidLumberTypeIdTest()
        {
            var command = new GetLumberTypeHistory(new Guid("b98496d9-63b1-e511-aec0-5cc5d43f6424"));
            command.Execute();
            var actual = command.Result;
            Assert.IsNotNull(actual);
        }
    }
}
