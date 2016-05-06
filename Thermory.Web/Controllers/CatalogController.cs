﻿using System;
using System.Web.Mvc;
using Thermory.Business;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers
{
    public class CatalogController : Controller
    {
        public ActionResult Index(Guid? id)
        {
            var model = new CatalogModel
            {
                SelectedCategoryId = id,
                LumberCategories = CommandDirectory.Instance.GetAllLumberCategories(),
                MiscellaneousCategories = CommandDirectory.Instance.GetAllMiscellaneousCategories()
            };
            return View(model);
        }

        public ActionResult Add()
        {
            var model = new CatalogModel
            {
                SelectedCategoryId = Guid.Empty,
                LumberCategories = CommandDirectory.Instance.GetAllLumberCategories(),
                MiscellaneousCategories = CommandDirectory.Instance.GetAllMiscellaneousCategories()
            };
            return View("Index", model);
        }
    }
}