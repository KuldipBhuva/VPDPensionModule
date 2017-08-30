using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PensionModel.ViewModel;
using PensionServices;
using PensionModel;

namespace PensionModule.Controllers
{
    public class NomineeController : Controller
    {
        //
        // GET: /Nominee/
        NomineeServices lstservices = new NomineeServices();
        NomineeViewModel lstmodel = new NomineeViewModel();

        public ActionResult Nominee()
        {
            if (Session["Comp"] != null)
            {
                if (Session["Emp"] != null)
                {
                    try
                    {
                        int id = Convert.ToInt32(Session["Emp"].ToString());
                        int compid = Convert.ToInt32(Session["Comp"].ToString());
                        lstmodel.lstnomei = lstservices.lstdata(compid, id);
                        return View(lstmodel);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    return RedirectToAction("../Employee/Index");
                }
            }
            else
            {
                return RedirectToAction("../Company/Index");
            }

        }

        [HttpPost]
        public ActionResult Nominee(NomineeViewModel model)
        {
            try
            {
                model.CompId = Convert.ToInt32(Session["Comp"].ToString());
                model.EmpId = Convert.ToInt32(Session["Emp"].ToString());
                if (model.hdnnomieId == 0)
                {
                    int a = lstservices.insert(model);
                }
                else
                {
                    int b = lstservices.update(model, model.hdnnomieId);
                }
                int id = Convert.ToInt32(Session["Emp"].ToString());
                int compid = Convert.ToInt32(Session["Comp"].ToString());
                lstmodel.lstnomei = lstservices.lstdata(compid, id);
                return View(lstmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetByID(NomineeViewModel model)
        {
            try
            {
                lstmodel = lstservices.GetByID(model.id);
                return Json(lstmodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
