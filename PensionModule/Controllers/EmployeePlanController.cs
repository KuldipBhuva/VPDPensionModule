using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pension.Models.ViewModel;
using Pension.Services;
using PensionModel.ViewModel;
using PensionServices;

namespace PensionModule.Controllers
{
    public class EmployeePlanController : Controller
    {
        //
        // GET: /EmployeePlan/


        EmployeePlanViewModel lstmodel = new EmployeePlanViewModel();
        EmployeePlanServices lstservices = new EmployeePlanServices();

        public ActionResult EmployeePlan()
        {
            if (Session["Comp"] != null)
            {
                if (Session["Emp"] != null)
                {
                    try
                    {
                        EmployeePlanViewModel model = new EmployeePlanViewModel();
                        //BindIns();
                        //lstmodel.ListIns = ViewBag.ListIns;
                        int id = Convert.ToInt32(Session["Emp"]);
                        model = lstservices.Data(id);
                        if (model == null)
                        {
                            ViewData["alert"] = "Please select Insurance and Plan in employee details";
                            return RedirectToAction("../Employee/Index");
                        }
                        else
                        {
                            model.lstempplan = lstservices.lstdata(id);
                            //lstmodel.LicId = 0;
                            //BindPlan(lstmodel);
                            return View(model);
                        }
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

        public JsonResult BindPlan(EmployeePlanViewModel model)
        {
            PlanServices insservices = new PlanServices();
            List<PlanViewModel> lstplan = new List<PlanViewModel>();
            int id = Convert.ToInt32(model.LicId);
            lstplan = insservices.lstPlanins();
            lstmodel.ListPlan = new List<PlanViewModel>();
            lstmodel.ListPlan.AddRange(lstplan);

            return Json(lstmodel.ListPlan, JsonRequestBehavior.AllowGet);
        }

        public void BindIns()
        {
            InsuranceService insservices = new InsuranceService();
            List<InsuranceViewModel> lstIns = new List<InsuranceViewModel>();

            lstIns = insservices.lstIns();
            lstmodel.ListIns = new List<InsuranceViewModel>();
            lstmodel.ListIns.AddRange(lstIns);
            ViewBag.ListIns = lstmodel.ListIns;
        }

        [HttpPost]
        public ActionResult EmployeePlan(EmployeePlanViewModel model)
        {
            try
            {
                model.EmpId = Convert.ToInt32(Session["Emp"]);
                if (model.hdnempplan == 0)
                {
                    int a = lstservices.insert(model, 1);
                }
                else
                {
                    int b = lstservices.insert(model, 0);
                }
                int id = Convert.ToInt32(Session["Emp"]);
                int cmy = Convert.ToInt32(Session["Comp"].ToString());

                ////BindPlan(model);
                ////lstmodel.ListPlan = ViewBag.ListPlan;
                //BindIns();
                //lstmodel.ListIns = ViewBag.ListIns;
                //lstmodel.LicId = 0;
                //BindPlan(lstmodel);

                lstmodel.lstempplan = lstservices.lstdata(id);
                return View(lstmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetByID(EmployeePlanViewModel model)
        {
            try
            {
                lstmodel = lstservices.GetByID(Convert.ToInt32(model.Id));
                //BindPlan(lstmodel);
                return Json(lstmodel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
