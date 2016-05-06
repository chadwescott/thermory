using System.Web.Mvc;
using Thermory.Domain.Models;

namespace Thermory.Web.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public object Save(UserProfile user)
        {
            return true;
        }
    }
}