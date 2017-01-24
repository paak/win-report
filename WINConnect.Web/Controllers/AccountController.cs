using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WINConnect.Data.Configuration.EntityFramework;
using WINConnect.Libs.Crypto;
using WINConnect.Models;

namespace WINConnect.Web.Controllers
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class AccountController : Controller
    {
        private WINContext db = new WINContext();

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)// && WebSecurity.Login(model.Username, model.Password, persistCookie: model.RememberMe))
            {
                string encryptedPassword = WINCrypto.Encrypt(model.Password);
                IQueryable<Contact> contact = db.Contacts
                    .AsNoTracking()
                    .Where(x =>
                        x.Roles.Any(y => y.Role.Name.Equals("administrator", StringComparison.OrdinalIgnoreCase)) &&
                        x.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase) &&
                        x.Memberships.Password.Equals(encryptedPassword, StringComparison.OrdinalIgnoreCase));

                if (contact != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
            }

            //FormsAuthentication.l

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Agent");
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Agent");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }
        #endregion

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
