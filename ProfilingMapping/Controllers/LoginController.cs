using ProfilingMapping.Models;
using ProfilingMapping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfilingMapping.Controllers
{
    public class LoginController : Controller
    {
        public static string errorMessage = string.Empty;
        public ActionResult Login()
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

        [HttpPost]
        public ActionResult Log_in(SystemModel systemModel)
        {
            Guid adminid = LoginRepository.HasAccess(systemModel.username, systemModel.password);

            if(adminid == Guid.Empty)
            {
                errorMessage = "Invalid Username or Password.. Please log in again.";
                return RedirectToAction("Login");
            }
            else
            {
                LogiInModel.adminID = adminid;
                return RedirectToAction("../Home/Index");
            }

        }
    }
}