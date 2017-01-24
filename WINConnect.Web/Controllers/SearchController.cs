using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;

namespace WINConnect.Web.Controllers
{
    public class SearchController : Controller
    {
        private WINContext db = new WINContext();

        // GET: /Search/Agent
        [HttpGet]
        public async Task<JsonResult> Agent(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return null;
            }
            term = term.Trim();
            if (term.Length < 3)
            {
                return null;
            }

            var agents = db.Agents
                    .Where(x => x.AgentName.Contains(term) && x.IsActivated)
                    .OrderBy(x => x.AgentName)
                    .Select(x => x.AgentName);

            return Json(await agents.ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        // GET: /Search/Country
        public async Task<JsonResult> Country(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return null;
            }
            term = term.Trim();
            if (term.Length < 3)
            {
                return null;
            }

            var countries = db.Countries
                    .Where(x => x.Name.Contains(term))
                    .OrderBy(x => x.Name)
                    .Select(x => x.Name);

            return Json(await countries.ToListAsync(), JsonRequestBehavior.AllowGet);
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
