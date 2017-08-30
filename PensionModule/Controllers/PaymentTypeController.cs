using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PensionModel.ViewModel;
using PensionServices;

namespace PensionModule.Controllers
{
    public class PaymentTypeController : Controller
    {
        //
        // GET: /PaymentType/

        PaymentTypeServices lstservices = new PaymentTypeServices();
        PaymentTypeModel lstmodel = new PaymentTypeModel();

        public ActionResult PaymentType()
        {
            lstmodel.lstpay = lstservices.lstpaytype();
            return View(lstmodel);
            return View();
        }

        [HttpPost]
        public ActionResult PaymentType(PaymentTypeModel model)
        {
            try
            {
                if (model.hdnpayid == 0)
                {
                    int a = lstservices.insert(model, 1);
                }
                else
                {
                    int b = lstservices.insert(model, 0);
                }
                //int id = Convert.ToInt32(Session["Emp"]);
                //int cmy = Convert.ToInt32(Session["Comp"].ToString());

                lstmodel.lstpay = lstservices.lstpaytype();
                return View(lstmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetByID(PaymentTypeModel model)
        {
            try
            {
                lstmodel = lstservices.GetByID(Convert.ToInt32(model.id));
                return Json(lstmodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
