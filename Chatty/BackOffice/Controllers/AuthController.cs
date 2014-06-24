using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Security;
using WebMatrix.WebData;
using Chatty.BackOffice.Filters;
using Chatty.BackOffice.Models;
using BackOffice.Providers;
using BackOffice.Common;
using BackOffice.Services;
using BackOffice.Dbo;
using BackOffice;

namespace Chatty.BackOffice.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AuthController : Controller
    {
        private UserService userService { get { return (UserService)Startup.container.Resolve(typeof(UserService), "UserService"); } }

        [AllowAnonymous]
        public ActionResult Index(string error = null)
        {
            var superAdminRes = userService.SearchFor(u => u.Username == Constants.SuperAdminLogin);
            if (superAdminRes.Count() == 0) // premier lancement
                userService.Insert(new User
                {
                    Username = Constants.SuperAdminLogin,
                    Password = Constants.SuperAdminPass,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    Email = "master@chatty.com",
                    isEnable = true,
                    Firstname = "Chatty",
                    Lastname = "Master",
                    isAdmin = true
                });

            if (WebSecurity.HasUserId)
                return RedirectToAction("Index", "Admin");
                
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

                return RedirectToAction("Index", new { Error="Login/Password are wrong" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { Error = "Login/Password are wrong" });

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(LoginModel model)
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Auth");
        }
    }
}
