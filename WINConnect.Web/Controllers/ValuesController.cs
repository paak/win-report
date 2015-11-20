using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class ValuesController : Controller
    {
        private WINContext db = new WINContext();
        //
        // GET: /Values
        [HttpGet]
        public JsonResult UNLocodes(string term, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(term) || string.IsNullOrWhiteSpace(term))
            {
                return null;
            }

            IQueryable<UNLocode> query = db.UNLocodes;

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.Trim();
                query = query.Where(x => x.Code.Contains(term) || x.Name.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(countryCode))
            {
                countryCode = countryCode.Trim();
                query = query.Where(x => x.CountryCode.Equals(countryCode));
            }
            query = query.OrderBy(x => x.Name);

            return Json(query, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult Countries(string term)
        {
            IQueryable<Country> query = db.Countries;
            //.Select(x => new
            //{
            //    value = x.Code,
            //    label = x.Name
            //});

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.Trim();
                query = query.Where(x => x.Code.Contains(term) || x.Name.Contains(term));
            }
            query = query.OrderBy(x => x.Name);

            return Json(query, JsonRequestBehavior.AllowGet);
        }

    }
}