using ProfilingMapping.Models;
using ProfilingMapping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfilingMapping.Controllers
{
    public class SettingsController : Controller
    {
        public ActionResult ProfileSettings()
        {
            if (LogiInModel.adminID == Guid.Empty)
            {
                LoginController.errorMessage = "No Session. Please Login...";
                return RedirectToAction("../Login/Login");
            }
            ViewBag.FullName = LogiInModel.FullName;

            LogiInModel logiInModel = new LogiInModel();

            logiInModel.CurrenUserProfile = SettingsRepository.getCurrentUserInfo(LogiInModel.adminID);
            logiInModel.BarangayList = BarangayRepository.GetAllBarangays();

            return View(logiInModel);
        }

        [HttpPost]
        public ActionResult SaveAccountChanges(TBL_ADMIN CurrenUserProfile)
        {
            Data data = new Data();
            
            TBL_ADMIN filter = new TBL_ADMIN();
            filter.ADMINID = CurrenUserProfile.ADMINID;
            CurrenUserProfile.ADMINID = new Guid();
            data.Update(CurrenUserProfile, filter, LogiInModel.adminID);
            return RedirectToAction("ProfileSettings");
        }
    }
}