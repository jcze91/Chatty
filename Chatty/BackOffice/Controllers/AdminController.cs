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

namespace Chatty.BackOffice.Controllers
{
    [InitializeSimpleMembership]
    public class AdminController : Controller
    {
        public ActionResult Index(string view)
        {
            AdminModel model = new AdminModel();

            if (!WebSecurity.HasUserId)// || !HttpContextDataAccessFactory.GetDataAccess().IsAuth(live.Name))
                return RedirectToAction("Login", "Auth");

           return View(model);
       
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Login", "Auth");
        }

    }
}
