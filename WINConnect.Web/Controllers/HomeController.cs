using System.Web.Mvc;
using WINConnect.Data;

namespace WINConnect.Web.Controllers
{
    public class HomeController : Controller
    {
        public UnitOfWork _uow = new UnitOfWork();//{ get; set; }
        //private WINDbContext db = new WINDbContext();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var agents = _uow.AgentRepository.Get();

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
    }
}
