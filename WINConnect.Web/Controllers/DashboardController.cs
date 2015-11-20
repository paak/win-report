using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;

namespace WINConnect.Web.Controllers
{
    public class DashboardController : Controller
    {
        private WINContext db = new WINContext();
        //
        // GET: /Dashboard/
        public JsonResult Index()
        {
            var c1 = db.Agents.Where(x => x.IsActivated).Count();
            var c2 = db.ViewMAWBs.Where(x => x.MawbSentOn != null).Count();
            var c3 = db.AirBookings.Where(x => x.SentOn != null).Count();
            var c4 = db.SeaBookings.Where(x => x.SourceId == 2 && x.CarrierSCAC != "CA20").Count();

            Stats stats = new Stats(c1, c2, c3, c4);
            return Json(stats, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Stats
        /// </summary>
        private class Stats
        {
            public int AgentsCount { get; set; }
            public int FWBCount { get; set; }
            public int FFRCount { get; set; }
            public int SeaBookingCount { get; set; }

            public Stats(int agentsCount, int fwbCount, int ffrCount, int seaBookingCount)
            {
                AgentsCount = agentsCount;
                FWBCount = fwbCount;
                FFRCount = ffrCount;
                SeaBookingCount = seaBookingCount;
            }
        }
    }
}