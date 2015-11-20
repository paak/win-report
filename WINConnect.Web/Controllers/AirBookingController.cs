using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WINConnect.Data;
using WINConnect.Libs.Extensions;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class AirBookingController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();
        //
        // GET: /AirBooking/
        public ActionResult Index(string agentname, string country, string refNumber, string carrier,
            DateTime? fromDate, DateTime? toDate, string sort, string sortDir, int page = 1, int pageSize = 15)
        {
            IQueryable<AirBooking> bookings = _uow.AirBookingRepository.Get();

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
                bookings = bookings.Where(x => x.AWBNumber.Contains(refNumber));
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

        //
        // GET: /AirBooking/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AirBooking/Create
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
        // GET: /AirBooking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /AirBooking/Edit/5
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
        // GET: /AirBooking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /AirBooking/Delete/5
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
