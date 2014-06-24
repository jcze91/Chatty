using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Chatty.BackOffice.Models;
using System.Collections.Generic;
using WebMatrix.WebData;
using Chatty.BackOffice.Filters;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using BackOffice.Common;
using BackOffice;
using BackOffice.Services;

namespace Chatty.BackOffice.Controllers
{
    [InitializeSimpleMembership]
    public class AdminController : Controller
    {
        private UserService userService { get { return (UserService)Startup.container.Resolve(typeof(UserService), "UserService"); } }

        public ActionResult Index(string view)
        {
            AdminModel model = new AdminModel();

            if (!WebSecurity.HasUserId)
                return RedirectToAction("Index", "Auth");
            var connectedAdmin = userService.SearchFor(u => u.Username == WebSecurity.CurrentUserName).First();
            UserModel connectedAdminModel = new UserModel
            {
                Username = connectedAdmin.Username,
                Id = connectedAdmin.Id
            };
            model.ConnectedAdmin = connectedAdminModel;

           return View(model);
       
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Auth");
        }

    }
}
