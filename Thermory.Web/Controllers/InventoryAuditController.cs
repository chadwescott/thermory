﻿using System.Web.Mvc;
using Thermory.Business;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers
{
    public class InventoryAuditController : Controller
    {
        public ActionResult Index()
        {
            var model = new InventoryWorksheet
            {
                LumberProductCategories = CommandDirectory.Instance.GetAllLumberProductsWithInventory()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Inventory[] inventory)
        {
            var model = new InventoryWorksheet
            {
                LumberProductCategories = CommandDirectory.Instance.GetAllLumberProductsWithInventory()
            };
            return View(model);
        }
    }
}
