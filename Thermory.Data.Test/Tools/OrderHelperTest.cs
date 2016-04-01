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
        private OrderMiscellaneousLineItem _miscLineItem1;
        private OrderMiscellaneousLineItem _miscLineItem2;

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
            _miscLineItem1 = new OrderMiscellaneousLineItem { MiscellaneousProductId = Guid.NewGuid() };
            _miscLineItem2 = new OrderMiscellaneousLineItem { MiscellaneousProductId = Guid.NewGuid() };
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

        private static void CheckIfLumberLineItemIsinList(List<OrderLumberLineItem> lineItems, OrderLumberLineItem lineItem)
        {
            Assert.IsTrue(lineItems.Contains(lineItem));
        }

        #region GetCreatedOrderMiscellaneousLineItemsTests

        [TestMethod]
        public void GetCreatedMiscellaneousLineItemsCreatedLineItemTest()
        {
            _order.OrderMiscellaneousLineItems.Add(_miscLineItem1);
            var orderMiscellaneousLineItems = new List<OrderMiscellaneousLineItem> { _miscLineItem2 };
            var createdMiscellaneousLineItems = OrderHelper.GetCreatedOrderMiscellaneousLineItems(_order, orderMiscellaneousLineItems).ToList();
            CheckIfMiscellaneousLineItemIsinList(createdMiscellaneousLineItems, _miscLineItem2);
        }

        [TestMethod]
        public void GetCreatedMiscellaneousLineItemsTwoCreatedLineItemsTest()
        {
            var orderMiscellaneousLineItems = new List<OrderMiscellaneousLineItem> { _miscLineItem1, _miscLineItem2 };
            var createdMiscellaneousLineItems = OrderHelper.GetCreatedOrderMiscellaneousLineItems(_order, orderMiscellaneousLineItems).ToList();
            CheckIfMiscellaneousLineItemIsinList(createdMiscellaneousLineItems, _miscLineItem1);
            CheckIfMiscellaneousLineItemIsinList(createdMiscellaneousLineItems, _miscLineItem2);
        }

        [TestMethod]
        public void GetCreatedMiscellaneousLineItemsOneOrderLineItemsTwoCreatedLineItemsTest()
        {
            _order.OrderMiscellaneousLineItems.Add(_miscLineItem1);
            var updatedMiscellaneousLineItems = new List<OrderMiscellaneousLineItem> { _miscLineItem1, _miscLineItem2 };
            var createdMiscellaneousLineItems = OrderHelper.GetCreatedOrderMiscellaneousLineItems(_order, updatedMiscellaneousLineItems).ToList();
            CheckIfMiscellaneousLineItemIsinList(createdMiscellaneousLineItems, _miscLineItem2);
        }

        [TestMethod]
        public void GetCreatedMiscellaneousLineItemsNoCreatedLineItemsTest()
        {
            _order.OrderMiscellaneousLineItems.Add(_miscLineItem1);
            _order.OrderMiscellaneousLineItems.Add(_miscLineItem2);
            var orderMiscellaneousLineItems = new List<OrderMiscellaneousLineItem> { _miscLineItem2 };
            var createdMiscellaneousLineItems = OrderHelper.GetCreatedOrderMiscellaneousLineItems(_order, orderMiscellaneousLineItems).ToList();
            Assert.IsFalse(createdMiscellaneousLineItems.Any());
        }

        #endregion

        #region GetEditedOrderMiscellaneousLineItemsTests

        [TestMethod]
        public void GetEditedOrderMiscellaneousLineItemsNoneEditedTest()
        {
            _order.OrderMiscellaneousLineItems.Add(_miscLineItem1);
            var orderMiscellaneousLineItems = new List<OrderMiscellaneousLineItem> { _miscLineItem2 };
            var editedMiscellaneousLineItems = OrderHelper.GetEditedOrderMiscellaneousLineItems(_order, orderMiscellaneousLineItems).ToList();
            Assert.IsFalse(editedMiscellaneousLineItems.Any());
        }

        [TestMethod]
        public void GetEditedOrderMiscellaneousLineItemsOneEditedTest()
        {
            _order.OrderMiscellaneousLineItems.Add(_miscLineItem1);
            var orderMiscellaneousLineItems = new List<OrderMiscellaneousLineItem> { _miscLineItem1 };
            var editedMiscellaneousLineItems = OrderHelper.GetEditedOrderMiscellaneousLineItems(_order, orderMiscellaneousLineItems).ToList();
            CheckIfMiscellaneousLineItemIsinList(editedMiscellaneousLineItems, _miscLineItem1);
        }

        [TestMethod]
        public void GetEditedOrderMiscellaneousLineItemsTwoEditedTest()
        {
            _order.OrderMiscellaneousLineItems.Add(_miscLineItem1);
            _order.OrderMiscellaneousLineItems.Add(_miscLineItem2);
            var orderMiscellaneousLineItems = new List<OrderMiscellaneousLineItem> { _miscLineItem1, _miscLineItem2 };
            var editedMiscellaneousLineItems = OrderHelper.GetEditedOrderMiscellaneousLineItems(_order, orderMiscellaneousLineItems).ToList();
            CheckIfMiscellaneousLineItemIsinList(editedMiscellaneousLineItems, _miscLineItem1);
            CheckIfMiscellaneousLineItemIsinList(editedMiscellaneousLineItems, _miscLineItem2);
        }

        #endregion

        private static void CheckIfMiscellaneousLineItemIsinList(List<OrderMiscellaneousLineItem> lineItems, OrderMiscellaneousLineItem lineItem)
        {
            Assert.IsTrue(lineItems.Contains(lineItem));
        }
    }
}
