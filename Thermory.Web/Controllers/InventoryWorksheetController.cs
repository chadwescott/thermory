﻿using System.Web.Mvc;
using Thermory.Business;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers
{
    public class InventoryWorksheetController : Controller
    {
        public ActionResult Index()
        {
            var model = new InventoryWorksheet
            {
                ProductFamilies = CommandDirectory.Instance.GetAllProductFamilies(),
                LumberFamilies = CommandDirectory.Instance.GetAllLumberFamilies()
            };
            return View(model);
        }
    }
}
