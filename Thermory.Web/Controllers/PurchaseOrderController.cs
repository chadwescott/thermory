﻿using System.Web.Mvc;
using Thermory.Domain.Enums;

namespace Thermory.Web.Controllers
{
    public class PurchaseOrderController : OrderController
    {
        protected override OrderTypes OrderType
        {
            get { return OrderTypes.PurchaseOrder; }
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}