using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Libs.Extensions;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class AgentController : Controller
    {
        private WINContext db = new WINContext();

        //
        // GET: /Agent/
        public ActionResult Index(string agentname, string country, string refNumber, string carrier,
            DateTime? fromDate, DateTime? toDate, string sort, string sortDir, int page = 1, int pageSize = 15)
        {
            IQueryable<Agent> agents = db.Agents;

            agentname = agentname.ToValueOrEmpty();
            if (!agentname.IsEmpty())
            {
                agents = agents.Where(x => x.AgentName.Contains(agentname));
            }

            country = country.ToValueOrEmpty();
            if (!country.IsEmpty())
            {
                agents = agents.Where(x => x.Country.Code.Contains(country) || x.Country.Name.Contains(country));
            }
            agents = agents.AsNoTracking();
            agents = agents.OrderByDescending(x => x.UpdatedOn);

            IPagedList<Agent> onePageOfAgents = agents.ToPagedList(page, pageSize);

            return View(onePageOfAgents);
        }

        //
        // GET: /Agent/Edit/5
        public ActionResult Edit(int id)
        {
            Agent agent = db.Agents.Find(id);

            // Registration Type
            var regType = db.ListValues.Where(x => x.Identifier == "RegistrationType");
            ViewBag.RegistrationTypeId = new SelectList(regType, "Id", "Name", agent.RegistrationTypeId);

            // Permissions
            var perms = db.Roles.OrderBy(x => x.Name);
            ViewBag.Roles = perms;

            return View(agent);
        }

        //
        // POST: /Agent/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Agent agentToUpdate)
        {
            Agent agent = db.Agents.Find(id);

            if (agent.AgentId != agentToUpdate.AgentId)
            {
                return View();
            }

            try
            {
                // TODO: Add update logic here
                agent.AccountNumber = agentToUpdate.AccountNumber;
                agent.Addrs1 = agentToUpdate.Addrs1;
                agent.AgentName = agentToUpdate.AgentName;
                agent.CityCode = agentToUpdate.CityCode;
                agent.CountryCode = agentToUpdate.CountryCode;
                agent.IATACode = agentToUpdate.IATACode;

                agent.IsActivated = agentToUpdate.IsActivated;
                agent.IsEYProgram = agentToUpdate.IsEYProgram;
                agent.RegistrationTypeId = agentToUpdate.RegistrationTypeId;

                db.Entry(agent).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
