using System.Linq;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;

namespace WINreport.Controllers
{
    public class FSUController : Controller
    {
        private WINContext db = new WINContext();

        //
        // GET: /FSU/
        public ActionResult Index(string awbPrefix, string awbNumber)
        {
            var fsu = db.FSUs
                .FirstOrDefault(x => x.AirlinePrefix == awbPrefix && x.AwbNumber == awbNumber);

            if (fsu == null)
            {
                return null;
            }

            var awbStatus = fsu.Details
                .OrderByDescending(x => x.MessageOn)
                .ToList();

            return View(awbStatus);
        }

        //
        // GET: /FSU/Details/5
        public ActionResult Details(int id)
        {
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
