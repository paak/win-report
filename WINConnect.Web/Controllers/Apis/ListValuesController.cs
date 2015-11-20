using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WINConnect.Data;
using WINConnect.Models;

namespace WINConnect.Web.Controllers.Apis
{
#if !DEBUG
    [Authorize]
#endif
    public class ListValuesController : ApiController
    {
        private UnitOfWork _uow = new UnitOfWork();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IQueryable<ListValues> Get(string id)
        {
            var listValues = _uow.ListValuesRepository.Get(x => x.Identifier == id).OrderBy(x => x.Name);
            return listValues;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}