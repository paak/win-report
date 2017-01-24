using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Libs.Extensions;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class SeaBookingController : Controller
    {
        private WINContext db = new WINContext();

        //
        // GET: /SeaBooking/
        public ActionResult Index(string agentname, string country, string refNumber, string carrier,
            DateTime? fromDate, DateTime? toDate, string sort, string sortDir, int page = 1, int pageSize = 15)
        {
            IQueryable<SeaBooking> seaBookings = db.SeaBookings;//.Includes(x;//.SeaBookingRepository.Get(includeProperties: "Carrier");

            // No WCA
            seaBookings = seaBookings.Where(x => !x.Created.Agent.AgentName.Contains("WCA"));
            // No Test Carrier
            seaBookings = seaBookings.Where(x => x.CarrierSCAC != "CA20");

            // Agent
            if (!string.IsNullOrWhiteSpace(agentname))
            {
                seaBookings = seaBookings.Where(x => x.Created.Agent.AgentName.Contains(agentname));
            }

            // Carrier
            if (!string.IsNullOrWhiteSpace(carrier))
            {
                seaBookings = seaBookings.Where(x =>
                    x.CarrierSCAC.Contains(carrier) ||
                    x.Carrier.Carrier.Name.Contains(carrier));
            }

            // Country
            if (!string.IsNullOrWhiteSpace(country))
            {
                seaBookings = seaBookings.Where(x =>
                    x.Created.Agent.CountryCode.Contains(country) ||
                    x.Created.Agent.Country.Name.Contains(country));
            }

            // Reference number
            if (!string.IsNullOrWhiteSpace(refNumber))
            {
                seaBookings = seaBookings.Where(x =>
                    //refNumber.Contains(x.BookingId.ToString()) ||
                    //x.BookingId.ToString().Contains(refNumber) ||
                    x.References.Any(y => y.Number.Contains(refNumber)));
            }

            // From date
            if (fromDate.IsValidDateTime())
            {
                seaBookings = seaBookings.Where(x => x.UpdatedOn.Year >= fromDate.Value.Year
                                                  && x.UpdatedOn.Month >= fromDate.Value.Month
                                                  && x.UpdatedOn.Day >= fromDate.Value.Day);
            }

            // To date
            if (toDate.IsValidDateTime())
            {
                seaBookings = seaBookings.Where(x => x.UpdatedOn.Year <= toDate.Value.Year
                                                  && x.UpdatedOn.Month <= toDate.Value.Month
                                                  && x.UpdatedOn.Day <= toDate.Value.Day);
            }

            // Sort ordering
            sort = sort.ToValueOrDefault("updated");
            sortDir = sortDir.ToValueOrDefault("desc");

            switch (sort)
            {
                case "country":
                    seaBookings =
                        sortDir == "asc"
                        ? seaBookings.OrderBy(x => x.Created.Agent.Country.Name)
                        : seaBookings.OrderByDescending(x => x.Created.Agent.Country.Name);
                    break;
                case "agent":
                    seaBookings =
                        sortDir == "asc"
                        ? seaBookings.OrderBy(x => x.Created.Agent.AgentName)
                        : seaBookings.OrderByDescending(x => x.Created.Agent.AgentName);
                    break;
                case "carrier":
                    seaBookings =
                        sortDir == "asc"
                        ? seaBookings.OrderBy(x => x.CarrierSCAC)
                        : seaBookings.OrderByDescending(x => x.CarrierSCAC);
                    break;
                case "status":
                    seaBookings =
                        sortDir == "asc"
                        ? seaBookings.OrderBy(x => x.Status.Name)
                        : seaBookings.OrderByDescending(x => x.Status.Name);
                    break;
                default:
                    seaBookings =
                        sortDir == "asc"
                        ? seaBookings.OrderBy(x => x.Created.UpdatedOn)
                        : seaBookings.OrderByDescending(x => x.UpdatedOn);
                    break;

            }

            IPagedList<SeaBooking> result = seaBookings.ToPagedList(page, pageSize);

            return View(result);
        }

        //
        // GET: /SeaBooking/Details/5
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
