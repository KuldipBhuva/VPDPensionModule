using Pension.Models.ViewModel;
using PensionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PensionModule.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        EmployeeService objService = new EmployeeService();
        EmployeeViewModel model = new EmployeeViewModel();
        public ActionResult Index()
        {
            int cid = 0;
            int uid = 0;
            if (Session["Comp"] != null)
            {
                cid = Convert.ToInt32(Session["Comp"].ToString());
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            
            List<EmployeeViewModel> lstEmp = new List<EmployeeViewModel>();
            lstEmp = objService.getRetireEmp(cid);
            model.ListEmp = new List<EmployeeViewModel>();
            model.ListEmp.AddRange(lstEmp);
            return View(model);
        }

    }
}
