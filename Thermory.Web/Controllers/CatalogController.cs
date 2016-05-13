﻿using System;
using System.Web.Mvc;
using Thermory.Business;
using Thermory.Domain.Models;
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

        [HttpPut]
        public ActionResult SaveLumberCategory(LumberCategory model)
        {
            CommandDirectory.Instance.SaveLumberCategory(model);
            return Json(model);
        }

        [HttpPut]
        public ActionResult SaveLumberProduct(LumberProduct model)
        {
            CommandDirectory.Instance.SaveLumberProduct(model);
            return Json(model);
        }

        [HttpPut]
        public ActionResult SaveLumberSubCategory(LumberSubCategory model)
        {
            CommandDirectory.Instance.SaveLumberSubCategory(model);
            return Json(model);
        }

        [HttpPut]
        public ActionResult SaveLumberType(LumberType model)
        {
            CommandDirectory.Instance.SaveLumberType(model);
            return Json(model);
        }
    }
}