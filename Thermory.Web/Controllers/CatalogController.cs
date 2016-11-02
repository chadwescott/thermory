using System;
using System.Web.Mvc;
using Thermory.Business;
using Thermory.Domain.Constants;
using Thermory.Domain.Models;
using Thermory.Web.Attributes;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers
{
    public class CatalogController : Controller
    {
        [Attributes.Authorize(Role.InventoryMaster)]
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

        [Attributes.Authorize(Role.InventoryMaster)]
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

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [Attributes.Authorize(Role.InventoryMaster)]
        public ActionResult SaveLumberCategory(LumberCategory model)
        {
            CommandDirectory.Instance.SaveLumberCategory(model);
            return Json(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [Attributes.Authorize(Role.InventoryMaster)]
        public ActionResult SaveLumberProduct(LumberProduct model)
        {
            CommandDirectory.Instance.SaveLumberProduct(model);
            return Json(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [Attributes.Authorize(Role.InventoryMaster)]
        public ActionResult SaveLumberSubCategory(LumberSubCategory model)
        {
            CommandDirectory.Instance.SaveLumberSubCategory(model);
            return Json(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [Attributes.Authorize(Role.InventoryMaster)]
        public ActionResult SaveLumberType(LumberType model)
        {
            CommandDirectory.Instance.SaveLumberType(model);
            return Json(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [Attributes.Authorize(Role.InventoryMaster)]
        public ActionResult SaveMiscellaneousCategory(MiscellaneousCategory model)
        {
            CommandDirectory.Instance.SaveMiscellaneousCategory(model);
            return Json(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [Attributes.Authorize(Role.InventoryMaster)]
        public ActionResult SaveMiscellaneousProduct(MiscellaneousProduct model)
        {
            CommandDirectory.Instance.SaveMiscellaneousProduct(model);
            return Json(model);
        }

        [HttpPost]
        [AjaxValidateAntiForgeryToken]
        [Attributes.Authorize(Role.InventoryMaster)]
        public ActionResult SaveMiscellaneousSubCategory(MiscellaneousSubCategory model)
        {
            CommandDirectory.Instance.SaveMiscellaneousSubCategory(model);
            return Json(model);
        }
    }
}