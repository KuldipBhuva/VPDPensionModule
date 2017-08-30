using System;
using System.Collections.Generic;
using System.Web.Mvc;


using Pension.Models.ViewModel;
using Pension.Services;
namespace PensionModule.Controllers
{
    public class AllowanceController : Controller
    {
        //
        // GET: /Allowance/
        AllowanceServices services = new AllowanceServices();
        private readonly List<AllowanceViewModel> lstReitre = new List<AllowanceViewModel>();

        public ActionResult Index()
        {
            try
            {

                AllowanceViewModel model = new AllowanceViewModel();
                //lstgrade = new List<GradeViewModel>();
                model.lstAllowace = services.lstAllow();
                return View(model);
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "AllowanceController", "Company", ex);
                return null;
            }
            
        }
        [HttpPost]
        public ActionResult Index(AllowanceViewModel model, string Button)
        {
            //lstgrade = new List<GradeViewModel>();
            try
            {
                switch (Button)
                {
                    case "Save":
                        if (model.HdnAlloid != 0)
                        {
                            int Update = services.UpdateAllowanceData(model.HdnAlloid, model);
                        }
                        else
                        {
                            int a = services.InsertAllow(model);
                        }
                        model.lstAllowace = services.lstAllow();
                        return View(model);

                    default:
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "AllowanceController", "Company", ex);
                return null;
            }

        }

        public JsonResult GetDataById(int id)
        {
            try
            {

                services = new AllowanceServices();
                AllowanceViewModel AlloModel = new AllowanceViewModel();
                AlloModel = services.GetById(id);
                return Json(AlloModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "AllowanceController", "Company", ex);
                return null;
            }
        }
        public JsonResult CheckExists(AllowanceViewModel model)
        {
            try
            {
                services = new AllowanceServices();
                AllowanceViewModel objLangModel = new AllowanceViewModel();
                objLangModel = services.CheckExists(model.AllowanceName);

                return Json(objLangModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "AllowanceController", "Company", ex);
                return null;
            }
        }
    }
}
