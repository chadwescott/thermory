using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Thermory.Domain;
using Thermory.Domain.Enums;

namespace Thermory.Data.Test
{
    [TestClass]
    public class DatabaseCommandDirectoryTest
    {
        [TestMethod]
        public void CreateOrderTest()
        {
            var lumberProduct = MockRepository.GenerateStub<ILumberProduct>();
            lumberProduct.Stub(s => s.Id).Return(new Guid("815642C9-08C6-E511-8274-5CC5D43F6424"));
            var lumberLineItem = MockRepository.GenerateStub<IOrderLumberLineItem>();
            lumberLineItem.Stub(s => s.LumberProduct).Return(lumberProduct);
            var lumberLineItems = new [] { lumberLineItem };

            var miscProduct = MockRepository.GenerateStub<IMiscellaneousProduct>();
            miscProduct.Stub(s => s.Id).Return(new Guid("D6888728-8DD5-E511-8E31-5CC5D43F6424"));
            var miscLineItem = MockRepository.GenerateStub<IOrderMiscellaneousLineItem>();
            miscLineItem.Stub(s => s.MiscellaneousProduct).Return(miscProduct);
            var miscLineItems = new[] { miscLineItem };

            DatabaseCommandDirectory.Instance.CreateOrder(1, OrderTypes.Purchase, lumberLineItems, miscLineItems);
        }
    }
}
