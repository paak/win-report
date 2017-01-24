using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Enum.Shipment;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class QuotesController : Controller
    {
        private WINContext db = new WINContext();
        //
        // GET: /Quote/
        public ActionResult Index(string agentname, string country, string refNumber, string carrier,
            string mode, ShipmentStatus? status,
            DateTime? fromDate, DateTime? toDate, string sort, string sortDir, int page = 1, int pageSize = 15)
        {
            IQueryable<Shipment> shipments = db.Shipments;

            if (!string.IsNullOrWhiteSpace(agentname))
            {
                agentname = agentname.ToLower().Trim();
                shipments = shipments
                    .Where(x => x.Exporter.Agent.AgentName.ToLower().Contains(agentname)
                        || x.RFQs.Any(y => y.Recipient.AgentName.ToLower().Contains(agentname)));
            }

            if (!string.IsNullOrWhiteSpace(country))
            {
                country = country.ToLower().Trim();

                shipments = shipments
                    .Where(x => x.Exporter.Agent.Country.Name.ToLower().Contains(country)
                        || x.RFQs.Any(y => y.Recipient.Country.Name.ToLower().Contains(country)));
            }

            if (!string.IsNullOrWhiteSpace(mode))
            {
                shipments = shipments.Where(x => x.TransportMode.Code == mode.ToString());
            }

            if (!string.IsNullOrWhiteSpace(null))
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
