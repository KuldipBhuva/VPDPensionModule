using Pension.Models.ViewModel;
using PensionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pension.Services;
using PensionModel.ViewModel;

namespace PensionModule.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        List<EmployeeViewModel> lstEmp = new List<EmployeeViewModel>();
        EmployeeService objService = new EmployeeService();
        EmployeeViewModel model = new EmployeeViewModel();
        int CompID = 0;
        public void BindRetirement()
        {
            try
            {
                objService = new EmployeeService();
                List<RetirementTypeViewModel> lstRetire = new List<RetirementTypeViewModel>();
                lstRetire = objService.lstRetirement();
                model.lstRetirement = new List<RetirementTypeViewModel>();
                model.lstRetirement.AddRange(lstRetire);
                ViewBag.lstRetirement = model.lstRetirement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult BindIns(EmployeeViewModel mod)
        {
            AnnuityServices insservices = new AnnuityServices();
            List<AnnuityViewModel> lstIns = new List<AnnuityViewModel>();
            int id = Convert.ToInt32(mod.PentionTypeID);
            lstIns = insservices.lstInsPlan(id);
            model.ListIns = new List<AnnuityViewModel>();
            model.ListIns.AddRange(lstIns);
            ViewBag.ListIns = model.ListIns;
            return Json(model.ListIns, JsonRequestBehavior.AllowGet);
        }

        public void BindPension()
        {
            PlanServices insservices = new PlanServices();
            List<PlanViewModel> Lstpen = new List<PlanViewModel>();
            //int id = Convert.ToInt32(mod.LicId);
            Lstpen = insservices.lstPlanins();
            model.Listpen = new List<PlanViewModel>();
            model.Listpen.AddRange(Lstpen);
            ViewBag.Listpen = model.Listpen;
        }

        public void BindGrade()
        {
            try
            {
                GradeServices insservices = new GradeServices();
                List<GradeViewModel> lstcom = new List<GradeViewModel>();
                lstcom = insservices.lstGrade();
                model.Listgrade = new List<GradeViewModel>();
                model.Listgrade.AddRange(lstcom);
                ViewBag.Listgrade = model.Listgrade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindPayType()
        {
            try
            {
                PaymentTypeServices insservices = new PaymentTypeServices();
                List<PaymentTypeModel> lstcom = new List<PaymentTypeModel>();
                lstcom = insservices.lstpaytype();
                model.Listpaytype = new List<PaymentTypeModel>();
                model.Listpaytype.AddRange(lstcom);
                ViewBag.Listpaytype = model.Listpaytype;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindCmy()
        {
            try
            {
                CompanyServices service = new CompanyServices();
                List<CompanyModel> lstmcom = new List<CompanyModel>();
                int cmy = Convert.ToInt32(Session["Comp"].ToString());
                lstmcom = service.lstMergeCmy(cmy);
                model.lstCmy = new List<CompanyModel>();
                model.lstCmy.AddRange(lstmcom);
                ViewBag.lstCmy = model.lstCmy;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BindEmployee(int CompID)
        {
            try
            {
                objService = new EmployeeService();
                lstEmp = new List<EmployeeViewModel>();
                lstEmp = objService.getAllEmp(CompID);
                model.ListEmp = new List<EmployeeViewModel>();
                model.ListEmp.AddRange(lstEmp);
                ViewBag.ListEmp = model.ListEmp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Index()
        {
            if (Session["Comp"] != null)
            {
                List<EmployeeViewModel> lstEmp = new List<EmployeeViewModel>();
                int rid = 0;

                if (Session["RID"] != null)
                {
                    rid = Convert.ToInt32(Session["RID"].ToString());
                    CompID = Convert.ToInt32(Session["Comp"].ToString());
                }
                BindRetirement();
                BindEmployee(CompID);
                BindGrade();
                model.Listgrade = ViewBag.Listgrade;
                BindPayType();
                model.Listpaytype = ViewBag.Listpaytype;
                BindCmy();
                model.lstCmy = ViewBag.lstCmy;
                BindPension();
                model.Listpen = ViewBag.Listpen;
                model.PentionTypeID = 0;
                BindIns(model);
                model.ListIns = ViewBag.ListIns;
                //lstEmp = objService.getAllEmp(CompID);
                //objModel.ListEmp = new List<EmployeeViewModel>();
                //objModel.ListEmp.AddRange(lstEmp);
                return View(model);
            }
            else
            {
                return RedirectToAction("../Company/Index");
            }
        }
        [HttpPost]
        public ActionResult Index(EmployeeViewModel model)
        {
            EmployeeService objService = new EmployeeService();
            int uid = 0;

            CompID = Convert.ToInt32(Session["Comp"].ToString());
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            model.Status = "1";
            model.CompId = Convert.ToInt32(Session["Comp"]);
            objService.insertEmp(model);
            BindRetirement();
            model.lstRetirement = ViewBag.lstRetirement;
            BindGrade();
            model.Listgrade = ViewBag.Listgrade;
            BindPayType();
            model.Listpaytype = ViewBag.Listpaytype;
            BindEmployee(CompID);
            model.ListEmp = ViewBag.ListEmp;
            BindCmy();
            model.lstCmy = ViewBag.lstCmy;
            BindPension();
            model.Listpen = ViewBag.Listpen;
            model.PentionTypeID = 0;
            BindIns(model);
            model.ListIns = ViewBag.ListIns;

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (Session["Comp"] != null)
            {
                Session["Emp"] = id;
                model = objService.GetbyID(id);
                CompID = Convert.ToInt32(Session["Comp"].ToString());
                if (model.RetireDate != null)
                {
                    DateTime dt = Convert.ToDateTime(model.RetireDate);
                    model.DueDate = dt.AddMonths(1);
                }
                BindRetirement();
                model.lstRetirement = ViewBag.lstRetirement;
                BindGrade();
                model.Listgrade = ViewBag.Listgrade;
                BindPayType();
                model.Listpaytype = ViewBag.Listpaytype;
                BindEmployee(CompID);
                model.ListEmp = ViewBag.ListEmp;
                BindCmy();
                model.lstCmy = ViewBag.lstCmy;
                BindPension();
                model.Listpen = ViewBag.Listpen;
                BindIns(model);
                model.ListIns = ViewBag.ListIns;
                return View(model);
            }
            else
            {
                return RedirectToAction("../Company/Index");
            }
        }
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel model)
        {
            int uid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            model.CompId = Convert.ToInt32(Session["Comp"]);
            objService.Update(model);
            BindRetirement();
            model.lstRetirement = ViewBag.lstRetirement;
            BindGrade();
            model.Listgrade = ViewBag.Listgrade;
            BindPayType();
            model.Listpaytype = ViewBag.Listpaytype;
            BindEmployee(CompID);
            model.ListEmp = ViewBag.ListEmp;
            BindCmy();
            model.lstCmy = ViewBag.lstCmy;
            BindPension();
            model.Listpen = ViewBag.Listpen;
            model.PentionTypeID = 0;
            BindIns(model);
            model.ListIns = ViewBag.ListIns;
            return RedirectToAction("Index", "Employee");
        }

        [HttpPost]
        public JsonResult CheckENoExists(EmployeeViewModel model)
        {
            int cc = Convert.ToInt32(Session["Comp"].ToString());
            model = objService.CheckExists(model.EmpNo,cc);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}
