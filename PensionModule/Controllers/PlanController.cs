using Pension.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pension;
using Pension.Services;

namespace Pension.Controllers
{
    public class PlanController : Controller
    {
        //
        // GET: /Plan/

        PlanServices services = new PlanServices();
        List<PlanViewModel> lstPlan = new List<PlanViewModel>();

        InsuranceService insservices = new InsuranceService();
        List<InsuranceViewModel> lstIns = new List<InsuranceViewModel>();
        public ActionResult Plan()
        {
            PlanViewModel model = new PlanViewModel();
            lstPlan = new List<PlanViewModel>();
            model.lstPlan = services.lstPlan();

            lstIns = insservices.lstIns();
            model.lstInsurance = new List<InsuranceViewModel>();
            model.lstInsurance.AddRange(lstIns);

            return View(model);
        }
        [HttpPost]
        public ActionResult Plan(PlanViewModel model, string Button)
        {
            lstPlan = new List<PlanViewModel>();

            switch (Button)
            {
                case "Save":
                    if (model.HdnPlanID != 0)
                    {
                        int Update = services.UpdateIns(model.HdnPlanID, model);
                    }
                    else
                    {
                        int a = services.InsertPlan(model);
                    }
                    model.lstPlan = services.lstPlan();
                    lstIns = insservices.lstIns();
                    model.lstInsurance = new List<InsuranceViewModel>();
                    model.lstInsurance.AddRange(lstIns);
                    return View(model);

                default:
                    return View(model);
            }

        }

        public JsonResult GetDataById(int id)
        {
            services = new PlanServices();
            PlanViewModel model = new PlanViewModel();
            model = services.GetById(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult Chkform(CompanyViewModel model)
        //{
        //    services = new CompanyServices();
        //    CompanyViewModel objLangModel = new CompanyViewModel();
        //    objLangModel = services.CheckExists(model.CompName);

        //    return Json(objLangModel, JsonRequestBehavior.AllowGet);
        //}

    }
}
