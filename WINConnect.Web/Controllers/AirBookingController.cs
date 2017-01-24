using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Libs.Extensions;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class AirBookingController : Controller
    {
        private WINContext db = new WINContext();
        //
        // GET: /AirBooking/
        public ActionResult Index(string agentname, string country, string refNumber, string carrier,
            DateTime? fromDate, DateTime? toDate, string sort, string sortDir, int page = 1, int pageSize = 15)
        {
            IQueryable<AirBooking> bookings = db.AirBookings;

            // No WCA
            bookings = bookings.Where(x => !x.AgentName.Contains("WCA"));

            if (!agentname.IsEmpty())
            {
                bookings = bookings.Where(x => x.AgentName.Contains(agentname));
            }

            if (!carrier.IsEmpty())
            {
                bookings = bookings.Where(x => x.AirlineName.Contains(carrier) || x.AirlinePrefix.Contains(carrier));
            }

            if (!country.IsEmpty())
            {
                bookings = bookings.Where(x => x.CountryName.Contains(country) || x.CountryCode.Contains(country));
            }

            if (!refNumber.IsEmpty())
            {
                refNumber = refNumber.RemoveSpecialCharacters();
                bookings = bookings.Where(x => refNumber.Contains(x.AirlinePrefix + x.AWBNumber));
            }
            // From date
            if (fromDate.IsValidDateTime())
            {
                bookings = bookings.Where(x => x.SentOn.Year >= fromDate.Value.Year
                                        && x.SentOn.Month >= fromDate.Value.Month
                                        && x.SentOn.Day >= fromDate.Value.Day);
            }

            // To date
            if (toDate.IsValidDateTime())
            {
                bookings = bookings.Where(x => x.SentOn.Year <= toDate.Value.Year
                                        && x.SentOn.Month <= toDate.Value.Month
                                        && x.SentOn.Day <= toDate.Value.Day);
            }

            bookings = bookings.OrderByDescending(x => x.SentOn);

            IPagedList<AirBooking> result = bookings.ToPagedList(page, pageSize);

            return View(result);
        }

        //
        // GET: /AirBooking/Details/5
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
