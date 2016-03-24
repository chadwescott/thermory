using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.Test
{
    [TestClass]
    public class DatabaseCommandDirectoryTest
    {
        [TestMethod]
        public void CreateOrderTest()
        {
            var lumberProduct = new LumberProduct {Id = new Guid("815642C9-08C6-E511-8274-5CC5D43F6424")};
            var lumberLineItem = new OrderLumberLineItem {LumberProduct = lumberProduct, Quantity = 2};
            var lumberLineItems = new [] { lumberLineItem };

            var miscProduct = new MiscellaneousProduct {Id = new Guid("D6888728-8DD5-E511-8E31-5CC5D43F6424")};
            var miscLineItem = new OrderMiscellaneousLineItem {MiscellaneousProduct = miscProduct, Quantity = 5};
            var miscLineItems = new[] { miscLineItem };

            DatabaseCommandDirectory.Instance.CreateOrder(1, OrderTypes.PurchaseOrder, lumberLineItems, miscLineItems);
        }
    }
}
