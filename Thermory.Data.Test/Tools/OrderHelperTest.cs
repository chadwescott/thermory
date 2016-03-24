using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thermory.Data.Tools;
using Thermory.Domain.Models;

namespace Thermory.Data.Test.Tools
{
    [TestClass]
    public class OrderHelperTest
    {
        private Order _order;
        private OrderLumberLineItem _lumberLineItem1;
        private OrderLumberLineItem _lumberLineItem2;

        [TestInitialize]
        public void TestInitialize()
        {
            _order = new Order
            {
                OrderLumberLineItems = new List<OrderLumberLineItem>(),
                OrderMiscellaneousLineItems = new List<OrderMiscellaneousLineItem>()
            };

            _lumberLineItem1 = new OrderLumberLineItem { LumberProductId = Guid.NewGuid() };
            _lumberLineItem2 = new OrderLumberLineItem { LumberProductId = Guid.NewGuid() };
        }

        #region GetCreatedOrderLumberLineItemsTests

        [TestMethod]
        public void GetCreatedLumberLineItemsCreatedLineItemTest()
        {
            _order.OrderLumberLineItems.Add(_lumberLineItem1);
            var orderLumberLineItems = new List<OrderLumberLineItem> {_lumberLineItem2};
            var createdLumberLineItems = OrderHelper.GetCreatedOrderLumberLineItems(_order, orderLumberLineItems).ToList();
            CheckIfLumberLineItemIsinList(createdLumberLineItems, _lumberLineItem2);
        }

        [TestMethod]
        public void GetCreatedLumberLineItemsTwoCreatedLineItemsTest()
        {
            var orderLumberLineItems = new List<OrderLumberLineItem> { _lumberLineItem1, _lumberLineItem2 };
            var createdLumberLineItems = OrderHelper.GetCreatedOrderLumberLineItems(_order, orderLumberLineItems).ToList();
            CheckIfLumberLineItemIsinList(createdLumberLineItems, _lumberLineItem1);
            CheckIfLumberLineItemIsinList(createdLumberLineItems, _lumberLineItem2);
        }

        [TestMethod]
        public void GetCreatedLumberLineItemsOneOrderLineItemsTwoCreatedLineItemsTest()
        {
            _order.OrderLumberLineItems.Add(_lumberLineItem1);
            var updatedLumberLineItems = new List<OrderLumberLineItem> { _lumberLineItem1, _lumberLineItem2 };
            var createdLumberLineItems = OrderHelper.GetCreatedOrderLumberLineItems(_order, updatedLumberLineItems).ToList();
            CheckIfLumberLineItemIsinList(createdLumberLineItems, _lumberLineItem2);
        }

        [TestMethod]
        public void GetCreatedLumberLineItemsNoCreatedLineItemsTest()
        {
            _order.OrderLumberLineItems.Add(_lumberLineItem1);
            _order.OrderLumberLineItems.Add(_lumberLineItem2);
            var orderLumberLineItems = new List<OrderLumberLineItem> { _lumberLineItem2 };
            var createdLumberLineItems = OrderHelper.GetCreatedOrderLumberLineItems(_order, orderLumberLineItems).ToList();
            Assert.IsFalse(createdLumberLineItems.Any());
        }

        #endregion

        #region GetEditedOrderLumberLineItemsTests

        [TestMethod]
        public void GetEditedOrderLumberLineItemsNoneEditedTest()
        {
            _order.OrderLumberLineItems.Add(_lumberLineItem1);
            var orderLumberLineItems = new List<OrderLumberLineItem> { _lumberLineItem2 };
            var editedLumberLineItems = OrderHelper.GetEditedOrderLumberLineItems(_order, orderLumberLineItems).ToList();
            Assert.IsFalse(editedLumberLineItems.Any());
        }

        [TestMethod]
        public void GetEditedOrderLumberLineItemsOneEditedTest()
        {
            _order.OrderLumberLineItems.Add(_lumberLineItem1);
            var orderLumberLineItems = new List<OrderLumberLineItem> { _lumberLineItem1 };
            var editedLumberLineItems = OrderHelper.GetEditedOrderLumberLineItems(_order, orderLumberLineItems).ToList();
            CheckIfLumberLineItemIsinList(editedLumberLineItems, _lumberLineItem1);
        }

        [TestMethod]
        public void GetEditedOrderLumberLineItemsTwoEditedTest()
        {
            _order.OrderLumberLineItems.Add(_lumberLineItem1);
            _order.OrderLumberLineItems.Add(_lumberLineItem2);
            var orderLumberLineItems = new List<OrderLumberLineItem> { _lumberLineItem1, _lumberLineItem2 };
            var editedLumberLineItems = OrderHelper.GetEditedOrderLumberLineItems(_order, orderLumberLineItems).ToList();
            CheckIfLumberLineItemIsinList(editedLumberLineItems, _lumberLineItem1);
            CheckIfLumberLineItemIsinList(editedLumberLineItems, _lumberLineItem2);
        }

        #endregion
        
        #region GetDeletedOrderLumberLineItemsTests

        [TestMethod]
        public void GetDeletedOrderLumberLineItemsNoneDeletedTest()
        {
            _order.OrderLumberLineItems.Add(_lumberLineItem1);
            var orderLumberLineItems = new List<OrderLumberLineItem> { _lumberLineItem1, _lumberLineItem2 };
            var deletedLumberLineItems = OrderHelper.GetDeletedOrderLumberLineItems(_order, orderLumberLineItems).ToList();
            Assert.IsFalse(deletedLumberLineItems.Any());
        }

        [TestMethod]
        public void GetDeletedOrderLumberLineItemsOneDeletedTest()
        {
            _order.OrderLumberLineItems.Add(_lumberLineItem1);
            _order.OrderLumberLineItems.Add(_lumberLineItem2);
            var orderLumberLineItems = new List<OrderLumberLineItem> { _lumberLineItem1 };
            var deletedLumberLineItems = OrderHelper.GetDeletedOrderLumberLineItems(_order, orderLumberLineItems).ToList();
            CheckIfLumberLineItemIsinList(deletedLumberLineItems, _lumberLineItem2);
        }

        [TestMethod]
        public void GetDeletedOrderLumberLineItemsTwoDeletedTest()
        {
            _order.OrderLumberLineItems.Add(_lumberLineItem1);
            _order.OrderLumberLineItems.Add(_lumberLineItem2);
            var orderLumberLineItems = new List<OrderLumberLineItem>();
            var deletedLumberLineItems = OrderHelper.GetDeletedOrderLumberLineItems(_order, orderLumberLineItems).ToList();
            CheckIfLumberLineItemIsinList(deletedLumberLineItems, _lumberLineItem1);
            CheckIfLumberLineItemIsinList(deletedLumberLineItems, _lumberLineItem2);
        }

        #endregion

        private static void CheckIfLumberLineItemIsinList(List<OrderLumberLineItem> lineItems, OrderLumberLineItem lineItem)
        {
            Assert.IsTrue(lineItems.Contains(lineItem));
        }
    }
}
