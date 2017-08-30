using Pension.Models.ViewModel;
using Pension.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pension.Controllers
{
    public class RetireTypeController : Controller
    {
        //
        // GET: /RetireType/

        RetirementTypeService services = new RetirementTypeService();
        //public readonly IGradeServices gradeService;
        //Any variable declared globally need to be readonly and create new instance in constructor.


        private readonly List<RetirementTypeViewModel> lstReitre = new List<RetirementTypeViewModel>();



        public ActionResult Retire()
        {
            RetirementTypeViewModel model = new RetirementTypeViewModel();
            //lstgrade = new List<GradeViewModel>();
            model.lstRetirement = services.lstRetirement();
            return View(model);
        }
        [HttpPost]
        public ActionResult Retire(RetirementTypeViewModel model, string Button)
        {
            //lstgrade = new List<GradeViewModel>();

            switch (Button)
            {
                case "Save":
                    if (model.HdnRetirementID != 0)
                    {
                        int Update = services.UpdateIns(model.HdnRetirementID, model);
                    }
                    else
                    {
                        int a = services.InsertPlan(model);
                    }
                    model.lstRetirement = services.lstRetirement();
                    return View(model);

                default:
                    return View(model);
            }

        }

        public JsonResult GetDataById(int id)
        {
            services = new RetirementTypeService();
            RetirementTypeViewModel CompModel = new RetirementTypeViewModel();
            CompModel = services.GetById(id);
            return Json(CompModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Chkform(RetirementTypeViewModel model)
        {
            services = new RetirementTypeService();
            RetirementTypeViewModel objLangModel = new RetirementTypeViewModel();
            objLangModel = services.CheckExists(model.RetirementType);

            return Json(objLangModel, JsonRequestBehavior.AllowGet);
        }


    }
}
