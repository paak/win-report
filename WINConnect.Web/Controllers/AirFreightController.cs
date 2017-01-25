using PagedList;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Libs.Extensions;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class AirFreightController : Controller
    {
        private WINContext db = new WINContext();
        //
        // GET: /AirFreight/
        //public ActionResult Index(DateTime? fromDate, DateTime? toDate, string airline, string awbNumber, string countryCode, string eAwb, string sort, string sortDir, int? agentId, int? page, int pageSize = 50)
        public ActionResult Index(string agentname, string country, string refNumber, string carrier,
            DateTime? fromDate, DateTime? toDate, string sort, string sortDir, int page = 1, int pageSize = 15)
        {
            IQueryable<ViewMAWB> mawbs = db.ViewMAWBs;

            if (!agentname.IsEmpty())
            {
                mawbs = mawbs.Where(x => x.AgentName.Contains(agentname));
            }

            if (!carrier.IsEmpty())
            {
                mawbs = mawbs.Where(x => x.AirlineName.Contains(carrier) || x.AirlinePrefix.Contains(carrier));
            }

            if (!country.IsEmpty())
            {
                mawbs = mawbs.Where(x => x.AgentCountry.Contains(country));
            }

            if (!refNumber.IsEmpty())
            {
                refNumber = refNumber.RemoveSpecialCharacters();
                mawbs = mawbs.Where(x => refNumber.Contains(x.AirlinePrefix + x.AwbNumber));
            }
            // From date
            if (fromDate.IsValidDateTime())
            {
                mawbs = mawbs.Where(x => x.MawbSentOn.Year >= fromDate.Value.Year
                                        && x.MawbSentOn.Month >= fromDate.Value.Month
                                        && x.MawbSentOn.Day >= fromDate.Value.Day);
            }else
            {
                fromDate = DateTime.UtcNow.AddDays(-14);
            }

            // To date
            if (toDate.IsValidDateTime())
            {
                mawbs = mawbs.Where(x => x.MawbSentOn.Year <= toDate.Value.Year
                                        && x.MawbSentOn.Month <= toDate.Value.Month
                                        && x.MawbSentOn.Day <= toDate.Value.Day);
            }
            else
            {
                toDate = DateTime.UtcNow;
            }

            mawbs = mawbs.OrderByDescending(x => x.MawbSentOn);

            IPagedList<ViewMAWB> result = mawbs.ToPagedList(page, pageSize);


            //int count = mawbs.Count();
            //mawbs = mawbs
            //    .OrderByDescending(x => x.UpdatedOn)
            //    .Skip((page - 1) * pageSize)// + 1)
            //    .Take(pageSize);

            return View(result);
        }

        //
        // GET: /AirFreight/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewMAWB mawb = await db.ViewMAWBs.FindAsync(id);
            if (mawb == null)
            {
                return HttpNotFound();
            }

            return View(mawb);
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
