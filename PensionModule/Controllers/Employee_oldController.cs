using Pension.Models.ViewModel;
using PensionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PensionModule.Controllers
{
    public class Employee_oldController : Controller
    {
        //
        // GET: /Employee/

        List<EmployeeViewModel> lstEmp = new List<EmployeeViewModel>();
        EmployeeService objService = new EmployeeService();
        EmployeeViewModel model = new EmployeeViewModel();
        public ActionResult Index()
        {
            objService = new EmployeeService();
            model = new EmployeeViewModel();
            int rid = 0;
            int CompID = 0;
            if (Session["RID"] != null)
            {
                rid = Convert.ToInt32(Session["RID"].ToString());
            }
            if (Convert.ToInt32(Session["Comp"].ToString()) != 0)
            {
                CompID = Convert.ToInt32(Session["Comp"].ToString());
            }
            else
            {
                return View("Company","Index");
            }
            

            BindRetirement();
            BindEmployee(CompID);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string Button, EmployeeViewModel model)
        {
            ModelState.Clear();
            int CompID = 0;
            CompID = Convert.ToInt32(Session["Comp"].ToString());

            switch (Button)
            {
                case "Save":
                    objService.InsertEmpoyeeData(model, 1);   // 1 flag For Insert
                    BindRetirement();
                    model.lstRetirement = ViewBag.lstRetirement;
                    BindEmployee(CompID);
                    model.ListEmp = ViewBag.ListEmp;
                    return View(model);
                case "Update":
                    objService.InsertEmpoyeeData(model, 2);   // 2 Flag for Update
                    BindRetirement();
                    model.lstRetirement = ViewBag.lstRetirement;
                    BindEmployee(CompID);
                    model.ListEmp = ViewBag.ListEmp;
                    return View(model);
                default:
                    return View(model);
            }

            // return View();
        }


        public ActionResult Edit(int id)
        {
            Session["Emp"] = id;
            int CompID = 0;
            objService = new EmployeeService();
            model = new EmployeeViewModel();
            CompID = Convert.ToInt32(Session["Comp"].ToString());

            model = objService.getEmpbyID(id);
            BindRetirement();
            model.lstRetirement = ViewBag.lstRetirement;
            BindEmployee(CompID);
            model.ListEmp = ViewBag.ListEmp;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel model)
        {
            ModelState.Clear();
            int CompID = 0;
            CompID = Convert.ToInt32(Session["Comp"].ToString());

            objService.InsertEmpoyeeData(model, 2);   // 2 Flag for Update
            BindRetirement();
            model.lstRetirement = ViewBag.lstRetirement;
            BindEmployee(CompID);
            model.ListEmp = ViewBag.ListEmp;
            return RedirectToAction("Index", "Employee");

        }

        // return View();





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

        public JsonResult GetDataById(int id)
        {
            objService = new EmployeeService();
            EmployeeViewModel model = new EmployeeViewModel();
            model = objService.getEmpbyID(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


    }
}
