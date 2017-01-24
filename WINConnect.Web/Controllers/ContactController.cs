using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class ContactController : Controller
    {
        private WINContext db = new WINContext();
        //
        // GET: /Contact/Agents
        public ActionResult Agents(int id)
        {
            var contacts = db.Contacts.Where(x => x.AgentId == id);
            return View(contacts);
        }

        //
        // GET: /Contact/Details/5
        public string Details(int id)
        {
            var contact = db.Contacts.Find(id);
            return (contact.Memberships == null) ? string.Empty : contact.Memberships.Password;
        }

        //
        // GET: /Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Contact/Create
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
        // GET: /Contact/Edit/5
        public ActionResult Edit(int id)
        {
            var contact = db.Contacts.Find(id);

            // ContactType Dropdown
            ViewBag.ContactType = new SelectList(SelectContactType(), "Key", "Value", contact.ContactType);

            // S/w company Dropdown
            var sw = db.Agents
                .Where(x => x.RegistrationType.Code == "WINSoftware" && x.IsActivated)
                .Select(x => new { Id = x.AgentId, Name = x.AgentName })
                .OrderBy(x => x.Name)
                .AsQueryable();

            ViewBag.OperatedBy = new SelectList(sw, "Id", "Name", contact.OperatedBy);

            return View(contact);
        }

        //
        // POST: /Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Contact contact)
        {
            try
            {
                // TODO: Add update logic here
                var contactToUpdate = db.Contacts.Find(id);
                if (contactToUpdate != null)
                {
                    contactToUpdate.ContactType = contact.ContactType;
                    contactToUpdate.FirstName = contact.FirstName;
                    contactToUpdate.LastName = contact.LastName;
                    contactToUpdate.Email = contact.Email;
                    contactToUpdate.Username = contact.Username;
                    contactToUpdate.OperatedBy = contact.OperatedBy;
                    contactToUpdate.IsActivated = contact.IsActivated;

                    db.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Agent", new { id = contact.AgentId });
            }
            catch
            {
                return View(contact);
            }
        }

        //
        // GET: /Contact/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Contact/Delete/5
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

        private IEnumerable<ContactType> SelectContactType()
        {
            List<ContactType> list = new List<ContactType>();

            list.Add(new ContactType { Key = "Primary", Value = "Primary" });
            list.Add(new ContactType { Key = "Information", Value = "Information" });
            list.Add(new ContactType { Key = "API", Value = "API" });

            return list;
        }

        public class ContactType
        {
            public string Key { get; set; }
            public string Value { get; set; }
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
