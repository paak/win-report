using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Libs.Extensions;
using WINConnect.Models;

namespace WINConnect.Api.Controllers
{
    public class AirFreightController : ApiController
    {
        private WINContext db = new WINContext();

        // GET: api/AirFreight
        public IHttpActionResult Get(
            string agentname, string country,
            string refNumber, string carrier,
            string origin, string destination,
            DateTime? fromDate, DateTime? toDate,
            string sort, string sortDir,
            int page = 1, int pageSize = 15)
        {
            IQueryable<ViewMAWB> mawbs = db.ViewMAWBs;

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
                refNumber = refNumber.RemoveSpecialCharacters();
                mawbs = mawbs.Where(x => refNumber.Contains(x.AirlinePrefix + x.AwbNumber));
            }

            if (!origin.IsEmpty())
            {
                mawbs = mawbs.Where(x => x.Origin == origin);
            }

            if (!destination.IsEmpty())
            {
                mawbs = mawbs.Where(x => x.Destination == destination);
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

            int count = mawbs.Count();
            mawbs = mawbs
                .OrderByDescending(x => x.MawbSentOn)
                .Skip((page - 1) * pageSize)// + 1)
                .Take(pageSize);

            var result = new
            {
                TotalCount = count,
                //totalPages = Math.Ceiling((double)count / pageSize),
                Awbs = mawbs
            };

            return Ok(result);
        }

        // GET: api/AirFreight/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AirFreight
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AirFreight/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AirFreight/5
        public void Delete(int id)
        {
        }

    }
}
