using System.Web.Mvc;
using Thermory.Business;
using Thermory.Domain.Constants;
using Thermory.Domain.Models;

namespace Thermory.Web.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles = Role.InventoryMaster)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPut]
        [Authorize(Roles = Role.InventoryMaster)]
        public object Save(UserProfile user)
        {
            CommandDirectory.Instance.UpdateUserRoles(user);
            return true;
        }
    }
}