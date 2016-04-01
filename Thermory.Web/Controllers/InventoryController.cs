﻿using System.Web.Mvc;
using Thermory.Business;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;
using Thermory.Web.Models;
using WebMatrix.WebData;

namespace Thermory.Web.Controllers
{
    public class InventoryController : Controller
    {
        public ActionResult Index()
        {
            var model = new InventoryModel
            {
                LumberCategories = CommandDirectory.Instance.GetAllLumberCategories(),
                MiscellaneousCategories = CommandDirectory.Instance.GetAllMiscellaneousCategories()
            };
            return View(model);
        }

        public ActionResult Audit()
        {
            var model = new InventoryModel
            {
                LumberCategories = CommandDirectory.Instance.GetAllLumberCategories(),
                MiscellaneousCategories = CommandDirectory.Instance.GetAllMiscellaneousCategories()
            };
            return View(model);
        }

        [HttpPost]
        public JsonResult Audit(LumberProduct[] lumberProducts, MiscellaneousProduct[] miscProducts)
        {
            var lumberInventory = lumberProducts ?? new LumberProduct[0];
            var miscInventory = miscProducts ?? new MiscellaneousProduct[0];
            CommandDirectory.Instance.UpdateProductInventory(WebSecurity.CurrentUserId, TransactionTypes.Audit,
                lumberInventory, miscInventory);
            return Json(new { status = "success" });
        }

    }
}
