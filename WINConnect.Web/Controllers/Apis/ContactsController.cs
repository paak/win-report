using System.Net;
using System.Net.Http;
using System.Web.Http;
using WINConnect.Data;
using WINConnect.Models;
using WINConnect.Web.Attributes;
namespace WINConnect.Web.Controllers.Apis
{
#if !DEBUG
    [Authorize]
#endif
    [RoutePrefix("api/contacts")]
    public class ContactsController : ApiController
    {
        public UnitOfWork _uow = new UnitOfWork();

        [HttpGet]
        [Route("{id}")]
        [Throttle(Name = "TestThrottle", Message = "You must wait {n} seconds before accessing this url again.", Seconds = 5)]
        public Contact Get(int id)
        {
            return (Contact)_uow.ContactRepository.GetById(id);
        }
    }
}