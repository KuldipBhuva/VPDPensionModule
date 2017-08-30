using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PensionModel.ViewModel;
using PensionServices;

namespace PensionModule.Controllers
{
    public class PensionTypeController : Controller
    {
        //
        // GET: /PensionType/

        PensionTypeViewModel lstmodel = new PensionTypeViewModel();
        PensionTypeServices lstservices = new PensionTypeServices();

        public ActionResult PensionType()
        {
            try
            {
                lstmodel.lstpen = lstservices.lstdata();
                return View(lstmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult PensionType(PensionTypeViewModel model)
        {
            try
            {
                if (model.hdnpentype == 0)
                {
                    int a = lstservices.insert(model, 1);
                }
                else
                {
                    int b = lstservices.insert(model, 0);
                }
                lstmodel.lstpen = lstservices.lstdata();
                return View(lstmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetByID(PensionTypeViewModel model)
        {
            try
            {
                lstmodel = lstservices.GetByID(Convert.ToInt32(model.Id));
                return Json(lstmodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
