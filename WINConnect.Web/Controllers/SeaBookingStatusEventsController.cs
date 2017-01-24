using System.Linq;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;

namespace WINConnect.Web.Controllers
{
    public class SeaBookingStatusEventsController : Controller
    {
        private WINContext db = new WINContext();

        //
        // GET: /SeaBookingStatusEvents/
        public ActionResult Index(int bookingId)
        {
            var statusEvents = db.SeaBookingStatusEvents
                .FirstOrDefault(x => x.BookingId == bookingId);

            return View(statusEvents);
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