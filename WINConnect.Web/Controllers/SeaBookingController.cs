using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WINConnect.Data;
using WINConnect.Libs.Extensions;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class SeaBookingController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();

        //
        // GET: /SeaBooking/
        public ActionResult Index(string agentname, string country, string refNumber, string carrier,
            DateTime? fromDate, DateTime? toDate, string sort, string sortDir, int page = 1, int pageSize = 15)
        {
            IQueryable<SeaBooking> seaBookings = _uow.SeaBookingRepository.Get(includeProperties: "Carrier");

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

        //
        // GET: /SeaBooking/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SeaBooking/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SeaBooking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SeaBooking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SeaBooking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SeaBooking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
