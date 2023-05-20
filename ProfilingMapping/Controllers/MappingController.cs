using ProfilingMapping.Models;
using ProfilingMapping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfilingMapping.Controllers
{
    public class MappingController : Controller
    {
        public ActionResult Mapping()
        {
            try
            {
                if (LogiInModel.adminID == Guid.Empty)
                {
                    return RedirectToAction("Login");
                }

                LogiInModel LoginModel = new LogiInModel();
                ViewBag.FullName = LogiInModel.FullName;
                LoginModel.ListOfNames = NamesRepository.GetAllNames();
                LoginModel.CurrenUserProfile = SettingsRepository.getCurrentUserInfo(LogiInModel.adminID);

                return View(LoginModel);
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }
    }
}