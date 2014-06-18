using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Security;
using WebMatrix.WebData;
using Chatty.BackOffice.Filters;
using Chatty.BackOffice.Models;

namespace Chatty.BackOffice.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AuthController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string liveName, string error = null)
        {
            if (WebSecurity.HasUserId) // && HttpContextDataAccessFactory.GetDataAccess().IsAuth(liveName))
                return RedirectToAction("Messages", "Admin", new { LiveName = liveName });
                
            var model = new LoginModel { Error = error };

            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                    return RedirectToAction("Index", "Admin");

                return RedirectToAction("Login", new { Error="Login/Password are wrong" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", new { Error = "Login/Password are wrong" });

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(LoginModel model)
        {
            WebSecurity.Logout();

            return RedirectToAction("Login", "Auth");
        }
    }
}
