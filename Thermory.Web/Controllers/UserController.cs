using System.Web.Mvc;
using Thermory.Business;
using Thermory.Domain.Models;

namespace Thermory.Web.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPut]
        public object Save(UserProfile user)
        {
            CommandDirectory.Instance.UpdateUserRoles(user);
            return true;
        }
    }
}