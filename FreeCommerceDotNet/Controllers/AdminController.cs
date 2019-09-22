using System.Web.Mvc;

namespace FreeCommerceDotNet.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }
    }
}