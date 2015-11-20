using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WINConnect.Data;
using WINConnect.Libs.Extensions;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class AirFreightController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();
        //
        // GET: /AirFreight/
        //public ActionResult Index(DateTime? fromDate, DateTime? toDate, string airline, string awbNumber, string countryCode, string eAwb, string sort, string sortDir, int? agentId, int? page, int pageSize = 50)
        public ActionResult Index(string agentname, string country, string refNumber, string carrier,
            DateTime? fromDate, DateTime? toDate, string sort, string sortDir, int page = 1, int pageSize = 15)
        {
            IQueryable<ViewMAWB> mawbs = _uow.AirFreightRepository.Get();//.SearchMaster(fromDate, toDate, airline, awbNumber, countryCode, eAwb, sort, sortDir, agentId);

            if (!agentname.IsEmpty())
            {
                mawbs = mawbs.Where(x => x.AgentName.Contains(agentname));
            }

            if (!carrier.IsEmpty())
            {
                mawbs = mawbs.Where(x => x.AirlineName.Contains(carrier) || x.AirlinePrefix.Contains(carrier));
            }

            if (!country.IsEmpty())
            {
                mawbs = mawbs.Where(x => x.AgentCountry.Contains(country));
            }

            if (!refNumber.IsEmpty())
            {
                mawbs = mawbs.Where(x => x.AwbNumber.Contains(refNumber));
            }
            // From date
            if (fromDate.IsValidDateTime())
            {
                mawbs = mawbs.Where(x => x.MawbSentOn.Year >= fromDate.Value.Year
                                        && x.MawbSentOn.Month >= fromDate.Value.Month
                                        && x.MawbSentOn.Day >= fromDate.Value.Day);
            }

            // To date
            if (toDate.IsValidDateTime())
            {
                mawbs = mawbs.Where(x => x.MawbSentOn.Year <= toDate.Value.Year
                                        && x.MawbSentOn.Month <= toDate.Value.Month
                                        && x.MawbSentOn.Day <= toDate.Value.Day);
            }

            mawbs = mawbs.OrderByDescending(x => x.MawbSentOn);

            IPagedList<ViewMAWB> result = mawbs.ToPagedList(page, pageSize);


            //int count = mawbs.Count();
            //mawbs = mawbs
            //    .OrderByDescending(x => x.UpdatedOn)
            //    .Skip((page - 1) * pageSize)// + 1)
            //    .Take(pageSize);

            return View(result);
        }

        //
        // GET: /AirFreight/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /AirFreight/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AirFreight/Create
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
        // GET: /AirFreight/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /AirFreight/Edit/5
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
        // GET: /AirFreight/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /AirFreight/Delete/5
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
