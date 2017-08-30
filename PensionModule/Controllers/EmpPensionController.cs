using Pension.Models.ViewModel;
using PensionModel;
using PensionModel.ViewModel;
using PensionServices;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PensionModule.Controllers
{
    public class EmpPensionController : Controller
    {
        // Employee wise Pension Details
        EmpPensionServices services = new EmpPensionServices();
        List<EmployeePensionVM> lstemppension = new List<EmployeePensionVM>();
        EmployeePlanServices lstservices = new EmployeePlanServices();
        EmployeePensionVM model = new EmployeePensionVM();

        public ActionResult EmplyeePension()
        {
            model.lstemppension = null;

            int id = Convert.ToInt32(Session["Emp"]);
            model.lstempplan = lstservices.lstdata(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EmplyeePension(EmployeePensionVM model)
        {
            int comp = Convert.ToInt32(Session["Comp"]);
            int Emp = Convert.ToInt32(Session["Emp"]);

            lstemppension = services.Employeewisepension(Emp, model.Year);
            model.lstemppension = new List<EmployeePensionVM>();
            model.lstemppension.AddRange(lstemppension);
            //  return View(lstemppension);

            model.lstempplan = lstservices.lstdata(Emp);

            return View(model);
        }

        public ActionResult ViewPension()
        {
            int id = Convert.ToInt32(Session["Emp"]);
            model.lstempplan = lstservices.lstdata(id);
            return PartialView("_EmpPensionData", model);
        }
        [HttpPost]
        public ActionResult EmpPlan()
        {
            return RedirectToAction("EmplyeePension");
        }
    }
}