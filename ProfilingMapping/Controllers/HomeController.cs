using ProfilingMapping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfilingMapping.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(LogiInModel.adminID == Guid.Empty)
            {
                LoginController.errorMessage = "No Session. Please Login...";
                return RedirectToAction("../Login/Login");
            }
            ViewBag.FullName = LogiInModel.FullName;
            return View();
        }

        public ActionResult About()
        {
            if (LogiInModel.adminID == Guid.Empty)
            {
                LoginController.errorMessage = "No Session. Please Login...";
                return RedirectToAction("../Login/Login");
            }

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (LogiInModel.adminID == Guid.Empty)
            {
                LoginController.errorMessage = "No Session. Please Login...";
                return RedirectToAction("../Login/Login");
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}