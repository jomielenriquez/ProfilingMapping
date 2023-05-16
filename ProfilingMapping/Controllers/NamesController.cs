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

        public ActionResult EditScreen(Guid? NameID)
        {
            try
            {
                if (LogiInModel.adminID == Guid.Empty)
                {
                    return RedirectToAction("Login");
                }
                LogiInModel LoginModel = new LogiInModel();
                ViewBag.FullName = LogiInModel.FullName;

                if (NameID == null)
                {
                    ViewBag.EditScreenHeader = "Add Profile";
                    //new
                    LoginModel.SelectedNames = null;
                }
                else
                {
                    ViewBag.EditScreenHeader = "Edit Profile";
                    //Update
                    LoginModel.SelectedNames = NamesRepository.GetName(NameID);
                }

                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    LoginModel.ErrorMessage = ErrorMessage;
                    LoginModel.SelectedNames = holdNames;
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
            string message = data.Delete(new TBL_NAMES(), LoginModel.AreChecked, "NAMEID");

            return RedirectToAction("ListScreen");
        }

        [HttpPost]
        public ActionResult Update(TBL_NAMES SelectedNames)
        {
            var result = string.Empty;
            Data data = new Data();
            if (SelectedNames.NAMEID == new Guid())
            {
                //Save
                SelectedNames.CREATEDBY = LogiInModel.adminID.ToString();

                result = data.Save(SelectedNames, new List<string> { "NAMEID" }, "NAMEID");
            }
            else
            {
                //Update
                TBL_NAMES Names = SelectedNames;
                TBL_NAMES filter = new TBL_NAMES();
                filter.NAMEID = Names.NAMEID;
                Names.NAMEID = new Guid();
                result = data.Update(Names, filter, LogiInModel.adminID);
            }
            if (result != "Success")
            {
                Guid x;
                if (!Guid.TryParse(result, out x)) // check if return value is not UID
                {
                    ErrorMessage = result;
                    holdNames = SelectedNames as TBL_NAMES;
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            ErrorMessage = string.Empty;
            holdNames = new TBL_NAMES();
            return RedirectToAction("ListScreen");
        }
    }
}