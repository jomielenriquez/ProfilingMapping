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
        static Guid holdSelectedName = new Guid();
        static string holdSelectedFullname = string.Empty;
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
                if (LoginModel.Role == "ADMIN USER")
                {
                    LoginModel.ListOfNames = NamesRepository.GetAllNames();
                }
                else
                {
                    LoginModel.ListOfNames = NamesRepository.GetAllNamesUsingBarangayID(LoginModel.Barangay);
                }
                LoginModel.CurrenUserProfile = SettingsRepository.getCurrentUserInfo(LogiInModel.adminID);
                if(holdSelectedName != Guid.Empty)
                {
                    LoginModel.SelectedRoute = holdSelectedName;
                    LoginModel.selectedName = holdSelectedFullname;
                }

                return View(LoginModel);
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult SelectSpecific(Guid? SelectedRoute)
        {
            if(SelectedRoute == null)
            {
                return RedirectToAction("SelectAll");
            }
            holdSelectedName = (Guid)SelectedRoute;
            PROFILINGMAPPINGDBEntities entities = new PROFILINGMAPPINGDBEntities();
            var selectedName = (from entNames in entities.TBL_NAMES.Where(row => row.NAMEID == SelectedRoute) select entNames).FirstOrDefault();

            if (selectedName != null)
            {
                holdSelectedFullname = selectedName.FIRSTNAME + selectedName.LASTNAME;
            }

            return RedirectToAction("Mapping");
        }
        public ActionResult SelectAll()
        {
            holdSelectedName = new Guid();
            holdSelectedFullname = string.Empty;
            return RedirectToAction("Mapping");
        }
    }
}