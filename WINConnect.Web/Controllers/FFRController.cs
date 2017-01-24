using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Models;

namespace WINreport.Controllers
{
    [RoutePrefix("FFR")]
    public class FFRController : Controller
    {
        private WINContext db = new WINContext();
        //
        // GET: /FFR/
        public ActionResult Index(string bookingId)
        {
            IEnumerable<FWBLog> fwbLog = db.FWBLog
                .Where(x => x.WIN_JobReferenceNumber == bookingId)
                .OrderByDescending(x => x.SentOn);

            return View(fwbLog);
        }

        /// <summary>
        /// GET File content
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void Details(string fileName, string filePath)
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + fileName + ".txt\"");
            Response.TransmitFile(filePath + fileName);
            Response.End();
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