using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Data.Commands;

namespace Thermory.Data.Test
{
    [TestClass]
    public class GetLumberFamilyByIdTest
    {
        [TestMethod]
        public void ValidLumberFamilyIdTest()
        {
            var id = new Guid("83715875-65B1-E511-AEC0-5CC5D43F6424");
            var target = new GetAllLumberProducts(id);
            target.Execute();
            var actual = target.Result;
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ValidProductFamilyIdTest()
        {
            var id = new Guid("6E5E9034-65B1-E511-AEC0-5CC5D43F6424");
            var target = new GetAllLumberProducts(id);
            target.Execute();
            var actual = target.Result;
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void InvalidIdTest()
        {
            var id = Guid.Empty;
            var target = new GetAllLumberProducts(id);
            target.Execute();
            var actual = target.Result;
            Assert.IsNull(actual);
        }
    }
}
