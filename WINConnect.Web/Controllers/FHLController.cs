using System.Linq;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;

namespace WINConnect.Web.Controllers
{
    public class FHLController : Controller
    {
        private WINContext db = new WINContext();
        //
        // GET: /FWB/
        public ActionResult Index(int mawbId)
        {
            int[] hawbIds = db.HAWBs.Where(x => x.MawbId == mawbId).Select(x => x.HawbId).ToArray();
            string[] hawbIdString = hawbIds.Select(x => x.ToString()).ToArray();

            var fhls = db.FWBLog.Where(x =>
                    hawbIdString.Contains(x.WIN_JobReferenceNumber) &&
                    x.MessageType.Name == "FHL"
                ).OrderByDescending(x => x.SentOn)
                .ToList();

            return View(fhls);
        }
    }
}