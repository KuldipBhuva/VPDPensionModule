using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PensionModel.ViewModel;
using PensionServices;

namespace PensionModule.Controllers
{
    public class IncentiveController : Controller
    {
        IncentiveViewModel lstmodel = new IncentiveViewModel();
        IncentiveServices lstservices = new IncentiveServices();

        public ActionResult Incentive()
        {
            lstmodel.lstin = lstservices.lstdata();
            return View(lstmodel);
        }

        [HttpPost]
        public ActionResult Incentive(IncentiveViewModel model)
        {
            if (model.hdnin == 0)
            {
                int a = lstservices.InsertUpdate(model, 1);
            }
            else
            {
                int b = lstservices.InsertUpdate(model, 0);
            }

            lstmodel.lstin = lstservices.lstdata();
            return View(lstmodel);
        }

        [HttpPost]
        public JsonResult GetByID(IncentiveViewModel model)
        {
            lstmodel = lstservices.GetByID(Convert.ToInt32(model.Id));
            return Json(lstmodel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetStatus(IncentiveViewModel model)
        {
            lstservices.status(model);
            Incentive();
            return Json(lstmodel, JsonRequestBehavior.AllowGet);
        }

    }
}
