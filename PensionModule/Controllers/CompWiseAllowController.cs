using Pension.Models.ViewModel;
using PensionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pension.Services;
using PensionModel.ViewModel;

namespace Pension.Controllers
{
    public class CompWiseAllowController : Controller
    {
        //
        // GET: /CompWiseAllow/
        CompWiseAlloService services = new CompWiseAlloService();
        List<CompWiseAllowViewModel> lstCompAllow = new List<CompWiseAllowViewModel>();

        CompanyServices insservices = new CompanyServices();
        List<CompanyModel> lstcom = new List<CompanyModel>();

        AllowanceServices insAlloser = new AllowanceServices();
        List<AllowanceViewModel> lstAllo = new List<AllowanceViewModel>();
        CompWiseAllowViewModel model = new CompWiseAllowViewModel();
        public ActionResult Index()
        {
            try
            {
                if (Session["Comp"] != null)
                {
                    //lstcom = insservices.lstCompany();
                    //model.lstComp = new List<CompanyModel>();
                    //model.lstComp.AddRange(lstcom);
                    int comp = Convert.ToInt32(Session["Comp"]);

                    lstAllo = services.lstAllow(comp, 0);
                    model.lstAllo = new List<AllowanceViewModel>();
                    model.lstAllo.AddRange(lstAllo);
                    BindGrade();
                    model.lstgrade = ViewBag.lstgrade;

                    lstCompAllow = services.lstCompAllow(comp);
                    model.lstCompAllo = new List<CompWiseAllowViewModel>();
                    model.lstCompAllo.AddRange(lstCompAllow);

                    return View(model);
                }
                else
                {
                    return RedirectToAction("../Company/Index");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindGrade()
        {
            try
            {
                GradeServices insservices = new GradeServices();
                List<GradeViewModel> lstcom = new List<GradeViewModel>();
                lstcom = insservices.lstGrade();
                model.lstgrade = new List<GradeViewModel>();
                model.lstgrade.AddRange(lstcom);
                ViewBag.lstgrade = model.lstgrade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Index(CompWiseAllowViewModel model, string Button)
        {
            lstCompAllow = new List<CompWiseAllowViewModel>();

            switch (Button)
            {
                case "Save":
                    if (model.hdnCompAlloid != 0)
                    {
                        int Update = services.UpdateAllowanceData(model.hdnCompAlloid, model);
                    }
                    else
                    {
                        foreach (var s in model.lstAllo)
                        {
                            if (s.IsChecked == true)
                            {
                                model.AllowanceID = s.id;
                                //model.IsChecked = s.IsChecked;
                                model.CompID = Convert.ToInt32(Session["Comp"]);
                                int a = services.InsertAllow(model);
                            }
                        }
                    }
                    lstcom = insservices.lstCompany();
                    model.lstComp = new List<CompanyModel>();
                    model.lstComp.AddRange(lstcom);

                    int comp = Convert.ToInt32(Session["Comp"]);

                    lstAllo = services.lstAllow(comp, 0);
                    model.lstAllo = new List<AllowanceViewModel>();
                    model.lstAllo.AddRange(lstAllo);

                    BindGrade();
                    model.lstgrade = ViewBag.lstgrade;

                    lstCompAllow = services.lstCompAllow(comp);
                    model.lstCompAllo = new List<CompWiseAllowViewModel>();
                    model.lstCompAllo.AddRange(lstCompAllow);
                    return View(model);

                default:
                    return View(model);
            }
        }

        [HttpPost]
        public JsonResult GetDataById(CompWiseAllowViewModel model)
        {
            int comp = Convert.ToInt32(Session["Comp"]);

            lstAllo = services.lstAllow(comp, model.id);
            model.lstAllo = new List<AllowanceViewModel>();
            model.lstAllo.AddRange(lstAllo);

            CompWiseAllowViewModel lstmodel = new CompWiseAllowViewModel();
            lstmodel = services.GetById(model.id);
            return Json(lstCompAllow, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult GetAllow(int id)
        {
            int comp = Convert.ToInt32(Session["Comp"]);
            lstAllo = services.lstAllow(comp, id);
            model.lstAllo = new List<AllowanceViewModel>();
            model.lstAllo.AddRange(lstAllo);

            return Json(model.lstAllo, JsonRequestBehavior.AllowGet);
        }
    }
}
