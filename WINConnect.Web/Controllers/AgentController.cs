using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using WINConnect.Data;
using WINConnect.Libs.Extensions;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class AgentController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();

        //
        // GET: /Agent/
        public ActionResult Index(string agentname, string country, string refNumber, string carrier,
            DateTime? fromDate, DateTime? toDate, string sort, string sortDir, int page = 1, int pageSize = 15)
        {
            IQueryable<Agent> agents = _uow.AgentRepository.Get();

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
            //agents = agents.OrderByDescending(x => x.Contacts.Any(y => y.Logins.Any(z => z.LoggedOn != null)));
            agents = agents.OrderByDescending(x => x.UpdatedOn);

            IPagedList<Agent> onePageOfAgents = agents.ToPagedList(page, pageSize);

            return View(onePageOfAgents);
        }

        //
        // GET: /Agent/Edit/5
        public ActionResult Edit(int id)
        {
            Agent agent = _uow.AgentRepository.GetById(id);

            // Registration Type
            var regType = _uow.ListValuesRepository.Get(x => x.Identifier == "RegistrationType");
            ViewBag.RegistrationTypeId = new SelectList(regType, "Id", "Name", agent.RegistrationTypeId);

            // Permissions
            var perms = _uow.RolesRepository.Get().OrderBy(x => x.Name);
            ViewBag.Roles = perms;

            return View(agent);
        }

        //
        // POST: /Agent/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Agent agentToUpdate)
        {
            Agent agent = _uow.AgentRepository.GetById(id);

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

                _uow.AgentRepository.Update(agent);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return null;
            }
            term = term.Trim();
            var agents = _uow.AgentRepository
                 .Get(x => x.AgentName.Contains(term))
                 .OrderBy(x => x.AgentName)
                 .Select(x => x.AgentName);

            return Json(agents, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
