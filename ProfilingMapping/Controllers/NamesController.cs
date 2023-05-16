using ProfilingMapping.Models;
using ProfilingMapping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfilingMapping.Controllers
{
    public class NamesController : Controller
    {
        static string ErrorMessage = string.Empty;
        static TBL_NAMES holdNames = new TBL_NAMES();
        // GET: AdminUser
        public ActionResult ListScreen()
        {
            try
            {
                // reset error Message and hold data when going to list screen
                ErrorMessage = string.Empty;
                holdNames = new TBL_NAMES();

                if (LogiInModel.adminID == Guid.Empty)
                {
                    return RedirectToAction("Login");
                }

                LogiInModel LoginModel = new LogiInModel();
                ViewBag.FullName = LogiInModel.FullName;
                LoginModel.ListOfNames = NamesRepository.GetAllNames();

                return View(LoginModel);
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }
    }
}