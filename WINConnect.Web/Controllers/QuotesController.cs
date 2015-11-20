using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WINConnect.Data;
using WINConnect.Enum.Shipment;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class QuotesController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();
        //
        // GET: /Quote/
        public ActionResult Index(string agentname, string country, string refNumber, string carrier,
            string mode, ShipmentStatus? status,
            DateTime? fromDate, DateTime? toDate, string sort, string sortDir, int page = 1, int pageSize = 15)
        {
            IQueryable<Shipment> shipments = _uow.ShipmentRepository.Get();

            if (!string.IsNullOrWhiteSpace(agentname))
            {
                shipments = shipments.Where(x => x.Title.Contains(agentname));
            }

            if (mode != null)
            {
                shipments = shipments.Where(x => x.TransportMode.Code == mode.ToString());
            }

            if (status != null)
            {
                /* Draft
                   Sent
                   Cancelled
                   Expired
                   Interested
                   Declined
                   New
                 * */
                if (status == ShipmentStatus.Active)
                {
                    shipments = shipments.Where(x => new string[] { "Interested", "New", "Sent" }.Contains(x.Status.Code));
                }
                else if (status == ShipmentStatus.Expired)
                {
                    shipments = shipments.Where(x => new string[] { "Cancelled", "Expired", "Declined" }.Contains(x.Status.Code));
                }
            }
            //shipments.Where(x => x.RFQs.Any(y => y.Status.Code == ""));
            shipments = shipments.OrderByDescending(x => x.UpdatedOn);

            IPagedList<Shipment> data = shipments.ToPagedList(page, pageSize);

            return View(data);
        }

        //
        // GET: /Quote/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Quote/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Quote/Create
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
        // GET: /Quote/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Quote/Edit/5
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
        // GET: /Quote/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Quote/Delete/5
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
