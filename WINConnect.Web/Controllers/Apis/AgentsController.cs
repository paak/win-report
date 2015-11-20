using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WINConnect.Data;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Models;

namespace WINConnect.Web.Controllers.Apis
{
#if !DEBUG
    [Authorize]
#endif
    public class AgentsController : ApiController
    {
        private UnitOfWork _uow = new UnitOfWork();
        private WINContext db = new WINContext();

        // GET api/<controller>
        [HttpGet]
        public IQueryable<Agent> Get(string search = "", string nearBy = "")
        {
            var agents = db.Agents
                .Include(a => a.Country)
                .Include(a => a.RegistrationType);

            if (!string.IsNullOrWhiteSpace(search))
            {
                agents = agents.Where(x => x.AgentName.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(nearBy))
            {
                agents = agents.Where(x =>
                    x.CityCode.Contains(nearBy) ||
                    x.Country.Code.Contains(nearBy) ||
                    x.Country.Name.Contains(nearBy));
            }

            agents = agents
                .OrderByDescending(x => x.UpdatedOn)
                .Take(10);

            //var agents = _uow.AgentRepository.Get(includeProperties: "RegistrationType").OrderByDescending(x => x.UpdatedOn).Take(10);
            return agents;
        }

        // GET api/<controller>/5
        [HttpGet]
        public Agent Get(int id)
        {
            //var agent = _uow.AgentRepository.GetById(id);
            return _uow.AgentRepository.GetById(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}