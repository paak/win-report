using System.Linq;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;

namespace WINConnect.Web.Controllers
{
    public class FWBController : Controller
    {
        private WINContext db = new WINContext();
        //
        // GET: /FWB/
        public ActionResult Index(int mawbId)
        {
            string mawbIdString = mawbId.ToString();
            var fwbs = db.FWBLog.Where(x =>
                    x.WIN_JobReferenceNumber == mawbIdString &&
                    x.MessageType.Name == "FWB"
                ).OrderByDescending(x => x.SentOn)
                .ToList();

            return View(fwbs);
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