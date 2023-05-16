﻿using ProfilingMapping.Models;
using ProfilingMapping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfilingMapping.Controllers
{
    public class RequestController : Controller
    {
        static string ErrorMessage = string.Empty;
        static TBL_REQUEST holdRequest = new TBL_REQUEST();

        public ActionResult ListScreen()
        {
            try
            {
                // reset error Message and hold data when going to list screen
                ErrorMessage = string.Empty;
                holdRequest = new TBL_REQUEST();

                if (LogiInModel.adminID == Guid.Empty)
                {
                    return RedirectToAction("Login");
                }

                LogiInModel LoginModel = new LogiInModel();
                ViewBag.FullName = LogiInModel.FullName;
                LoginModel.ListOfRequest = RequestRepository.GetAllRequest();

                return View(LoginModel);
            }
            catch
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult EditScreen(Guid? RequestID)
        {
            try
            {
                if (LogiInModel.adminID == Guid.Empty)
                {
                    return RedirectToAction("Login");
                }
                LogiInModel LoginModel = new LogiInModel();
                LoginModel.ListOfNames = NamesRepository.GetAllNames();
                ViewBag.FullName = LogiInModel.FullName;

                if (RequestID == null)
                {
                    ViewBag.EditScreenHeader = "Add Request";
                    //new
                    LoginModel.SelectedRequest = null;
                }
                else
                {
                    ViewBag.EditScreenHeader = "Edit Request";
                    //Update
                    LoginModel.SelectedRequest = RequestRepository.GetRequest(RequestID);
                }

                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    LoginModel.ErrorMessage = ErrorMessage;
                    LoginModel.SelectedRequest = holdRequest;
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
            string message = data.Delete(new TBL_REQUEST(), LoginModel.AreChecked, "REQUESTID");

            return RedirectToAction("ListScreen");
        }

        [HttpPost]
        public ActionResult Update(TBL_REQUEST SelectedRequest)
        {
            var result = string.Empty;
            Data data = new Data();
            if (SelectedRequest.REQUESTID == new Guid())
            {
                //Save
                SelectedRequest.CREATEDBY = LogiInModel.adminID.ToString();

                result = data.Save(SelectedRequest, new List<string> { "REQUESTID" }, "REQUESTID");
            }
            else
            {
                //Update
                TBL_REQUEST Request = SelectedRequest;
                TBL_REQUEST filter = new TBL_REQUEST();
                filter.REQUESTID = Request.REQUESTID;
                Request.REQUESTID = new Guid();
                result = data.Update(Request, filter, LogiInModel.adminID);
            }
            if (result != "Success")
            {
                Guid x;
                if (!Guid.TryParse(result, out x)) // check if return value is not UID
                {
                    ErrorMessage = result;
                    holdRequest = SelectedRequest as TBL_REQUEST;
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            ErrorMessage = string.Empty;
            holdRequest = new TBL_REQUEST();
            return RedirectToAction("ListScreen");
        }
    }
}