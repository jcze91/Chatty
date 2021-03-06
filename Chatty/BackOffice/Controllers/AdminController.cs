﻿using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using BackOffice.Models;
using System.Collections.Generic;
using WebMatrix.WebData;
using Chatty.BackOffice.Filters;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using BackOffice.Common;
using BackOffice;
using BackOffice.Services;
using BackOffice.Utils;

namespace Chatty.BackOffice.Controllers
{
    [InitializeSimpleMembership]
    public class AdminController : Controller
    {
        private UserService userService { get { return (UserService)Startup.container.Resolve(typeof(UserService), "UserService"); } }

        public ActionResult Index(string view)
        {
            AdminModel model = new AdminModel();
            model.RestURL = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + ":" + HttpContext.Request.Url.Port;

            if (!WebSecurity.HasUserId)
                return RedirectToAction("Index", "Auth");
            var connectedAdmin = userService.SearchFor(u => u.Username == WebSecurity.CurrentUserName).First();
            connectedAdmin.ConnexionDate = DateTime.Now;
            connectedAdmin.Token = (connectedAdmin.Username + connectedAdmin.ConnexionDate.ToString()).sha1();
            userService.Update(connectedAdmin);

            UserModel connectedAdminModel = new UserModel
            {
                Username = connectedAdmin.Username,
                Id = connectedAdmin.Id,
                Token = connectedAdmin.Token
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
