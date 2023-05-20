using ProfilingMapping.Models;
using ProfilingMapping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfilingMapping.Controllers
{
    public class AdminController : Controller
    {
        static string ErrorMessage = string.Empty;
        static TBL_ADMIN holdAdmin = new TBL_ADMIN();

        public ActionResult ListScreen()
        {
            try
            {
                // reset error Message and hold data when going to list screen
                ErrorMessage = string.Empty;
                holdAdmin = new TBL_ADMIN();

                if (LogiInModel.adminID == Guid.Empty)
                {
                    return RedirectToAction("Login");
                }

                LogiInModel LoginModel = new LogiInModel();
                ViewBag.FullName = LogiInModel.FullName;
                LoginModel.ListOfAdmins = SettingsRepository.getAdmins();

                return View(LoginModel);
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult EditScreen(Guid? AdminID)
        {
            try
            {
                if (LogiInModel.adminID == Guid.Empty)
                {
                    return RedirectToAction("Login");
                }
                LogiInModel LoginModel = new LogiInModel();
                LoginModel.BarangayList = BarangayRepository.GetAllBarangays();
                LoginModel.ListOfTaggings = TaggingRepository.getTaggings();
                ViewBag.FullName = LogiInModel.FullName;

                if (AdminID == null)
                {
                    ViewBag.EditScreenHeader = "Add User";
                    //new
                    LoginModel.SelectedAdmin = null;
                }
                else
                {
                    ViewBag.EditScreenHeader = "Edit User";
                    //Update
                    LoginModel.SelectedAdmin = SettingsRepository.getCurrentUserInfo(AdminID);
                }

                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    LoginModel.ErrorMessage = ErrorMessage;
                    LoginModel.SelectedAdmin = holdAdmin;
                }

                return View(LoginModel);
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult Delete(LogiInModel LoginModel)
        {
            Data data = new Data();
            string message = data.Delete(new TBL_ADMIN(), LoginModel.AreChecked, "ADMINID");

            return RedirectToAction("ListScreen");
        }

        [HttpPost]
        public ActionResult Update(TBL_ADMIN SelectedAdmin)
        {
            var result = string.Empty;
            Data data = new Data();
            if (SelectedAdmin.ADMINID == new Guid())
            {
                //Save
                SelectedAdmin.CREATEDBY = LogiInModel.adminID.ToString();
                SelectedAdmin.USERNAME = SelectedAdmin.FIRSTNAME.ToLower();
                SelectedAdmin.PASSWORD = SelectedAdmin.FIRSTNAME.ToLower();
                result = data.Save(SelectedAdmin, new List<string> { "ADMINID" }, "ADMINID");
            }
            else
            {
                //Update
                TBL_ADMIN Admin = SelectedAdmin;
                TBL_ADMIN filter = new TBL_ADMIN();
                filter.ADMINID = Admin.ADMINID;
                Admin.ADMINID = new Guid();
                result = data.Update(Admin, filter, LogiInModel.adminID);
            }
            if (result != "Success")
            {
                Guid x;
                if (!Guid.TryParse(result, out x)) // check if return value is not UID
                {
                    ErrorMessage = result;
                    holdAdmin = SelectedAdmin as TBL_ADMIN;
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            ErrorMessage = string.Empty;
            holdAdmin = new TBL_ADMIN();
            return RedirectToAction("ListScreen");
        }
    }
}