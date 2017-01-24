using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;

namespace WINConnect.Web.Controllers
{
    public class HomeController : Controller
    {
        public WINContext db = new WINContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var agents = db.Agents;

            return View(agents);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
