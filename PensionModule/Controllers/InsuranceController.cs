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
    public class InsuranceController : Controller
    {
        //
        // GET: /Insurance/

        InsuranceService services = new InsuranceService();
        List<InsuranceViewModel> lstIns = new List<InsuranceViewModel>();
        public ActionResult Insurance()
        {
            InsuranceViewModel model = new InsuranceViewModel();
            lstIns = new List<InsuranceViewModel>();
            model.lstins = services.lstIns();
            return View(model);
        }
        [HttpPost]
        public ActionResult Insurance(InsuranceViewModel model, string Button)
        {
            lstIns = new List<InsuranceViewModel>();

            switch (Button)
            {
                case "Save":
                    if (model.hdnInsuranceID != 0)
                    {
                        int Update = services.UpdateIns(model.hdnInsuranceID, model);
                    }
                    else
                    {
                        int a = services.InsertIns(model);
                    }
                    model.lstins = services.lstIns();
                    return View(model);

                default:
                    return View(model);
            }

        }

        public JsonResult GetDataById(int id)
        {
            services = new InsuranceService();
            InsuranceViewModel model = new InsuranceViewModel();
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
