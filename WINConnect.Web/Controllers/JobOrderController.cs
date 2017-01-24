using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class JobOrderController : Controller
    {
        private WINContext db = new WINContext();

        // GET: JobOrder
        public ActionResult Index(int page = 1, int pageSize = 25)
        {
            IQueryable<JobMaster> jobs = db.JobMaster.AsNoTracking();

            jobs = jobs.OrderByDescending(x => x.CreatedOn);


            // Preparing for PagedList
            IPagedList<JobMaster> paged = jobs.ToPagedList(page, pageSize);

            return View(paged);
        }
    }
}