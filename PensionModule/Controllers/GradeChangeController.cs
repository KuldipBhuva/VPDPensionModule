using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PensionModel.ViewModel;
using PensionServices;
using Pension.Models;
using Pension.Services;
using Pension.Models.ViewModel;
using PensionModel;

namespace PensionModule.Controllers
{
    public class GradeChangeController : Controller
    {
        //
        // GET: /GradeChange/

        GradeChangeViewModel lstmodel = new GradeChangeViewModel();
        GradeChangeServices lstservice = new GradeChangeServices();

        public ActionResult GradeChange()
        {
            if (Session["Comp"] != null)
            {
                if (Session["Emp"] != null)
                {
                    try
                    {
                        bindgrade();
                        lstmodel.listgrade = ViewBag.listgrade;

                        int id = Convert.ToInt32(Session["Emp"].ToString());
                        int compid = Convert.ToInt32(Session["Comp"].ToString());
                        lstmodel.lstgrade = lstservice.lstgrade(id, compid);
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

        //public ActionResult GetDate()
        //{
        //    DateTime startDate = System.DateTime.Now;
        //    DateTime expiryDate = startDate.AddDays(365);
        //}

        public void bindgrade()
        {
            try
            {
                GradeServices insservices = new GradeServices();
                List<GradeViewModel> lstcom = new List<GradeViewModel>();

                lstcom = insservices.lstGrade();
                lstmodel.listgrade = new List<GradeViewModel>();
                lstmodel.listgrade.AddRange(lstcom);
                ViewBag.listgrade = lstmodel.listgrade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult GradeChange(GradeChangeViewModel model)
        {
            try
            {
                if (model.GradeName != "no")
                {
                    model.EmpNo = Convert.ToInt32(Session["Emp"].ToString());
                    model.Compnay_id = Convert.ToInt32(Session["Comp"].ToString());
                    if (model.hdngradeId == 0)
                    {
                        int a = lstservice.insert(model);
                    }
                    else
                    {
                        int b = lstservice.update(model, model.hdngradeId);
                    }
                }
                bindgrade();
                lstmodel.listgrade = ViewBag.listgrade;

                int id = Convert.ToInt32(Session["Emp"].ToString());
                int compid = Convert.ToInt32(Session["Comp"].ToString());
                lstmodel.lstgrade = lstservice.lstgrade(id, compid);
                return View(lstmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult CheckExists(GradeChangeViewModel model)
        {
            try
            {
                int id = Convert.ToInt32(Session["Emp"].ToString());
                lstmodel = lstservice.CheckExists(Convert.ToInt32(model.GradeChangeTo), id);
                return Json(lstmodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult GetByID(GradeChangeViewModel model)
        {
            try
            {
                lstmodel = lstservice.GetByID(model.id);
                return Json(lstmodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
