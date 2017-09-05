using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using PensionModel;
using PensionModel.ViewModel;
using PensionServices;
using System.Data.Common;
using System.Data.Entity;
using System.ComponentModel;
using System.Data.SqlClient;

namespace PensionModule.Controllers
{
    public class ReportController : Controller
    {
        ReportServices lstservices = new ReportServices();
        ReportViewModel lstmodel = new ReportViewModel();
        List<ReportViewModel> lstdata = new List<ReportViewModel>();

        public ActionResult PolicyReport()
        {
            if (Session["Comp"] != null)
            {
                int cmp = Convert.ToInt32(Session["Comp"].ToString());
                lstdata = lstservices.lstpolicydata(cmp);
                lstmodel.lstempplan = new List<ReportViewModel>();
                lstmodel.lstempplan.AddRange(lstdata);

                return View(lstmodel);
            }
            else
            {
                return RedirectToAction("../Company/Index");
            }
        }

        public ActionResult EmployeeList()
        {
            if (Session["Comp"] != null)
            {
                int cmp = Convert.ToInt32(Session["Comp"].ToString());
                lstdata = lstservices.lstempdata(cmp);
                lstmodel.lstemp = new List<ReportViewModel>();
                lstmodel.lstemp.AddRange(lstdata);

                return View(lstmodel);
            }
            else
            {
                return RedirectToAction("../Company/Index");
            }
        }

        public ActionResult SalaryDetails()
        {
            try
            {
                if (Session["Comp"] != null)
                {
                    if (Session["Emp"] != null)
                    {
                        using (var ctx = new VPDPensionEntities())
                        using (var cmd = ctx.Database.Connection.CreateCommand())
                        {
                            int emp = Convert.ToInt32(Session["Emp"].ToString());
                            int cmp = Convert.ToInt32(Session["Comp"].ToString());
                            ctx.Database.Connection.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Get_SalaryDetails";

                            //cmd.Parameters.Add(new SqlParameter("@year", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, "2016-2017"));
                            cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.Int, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, emp));
                            cmd.Parameters.Add(new SqlParameter("@compid", SqlDbType.Int, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cmp));
                            using (var reader = cmd.ExecuteReader())
                            {
                                var model = this.lstservices.Read(reader).ToList();
                                return View(model);
                            }
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Illustration()
        {
            return View();
        }

        public ActionResult PensionReport()
        {
            return View();
        }

        public ActionResult SAReport()
        {
            try
            {
                if (Session["Comp"] != null)
                {
                    if (Session["Emp"] != null)
                    {
                        using (var ctx = new VPDPensionEntities())
                        using (var cmd = ctx.Database.Connection.CreateCommand())
                        {
                            int emp = Convert.ToInt32(Session["Emp"].ToString());
                            int cmp = Convert.ToInt32(Session["Comp"].ToString());
                            int preyr = 2016;
                            int crtyr = 2017;
                            string accyr = "2016-2017";
                            ctx.Database.Connection.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Get_SAData";

                            cmd.Parameters.Add(new SqlParameter("@accyr", SqlDbType.Text, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, accyr));
                            cmd.Parameters.Add(new SqlParameter("@preyr", SqlDbType.Int, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, preyr));
                            cmd.Parameters.Add(new SqlParameter("@crtyr", SqlDbType.Int, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, crtyr));
                            cmd.Parameters.Add(new SqlParameter("@empid", SqlDbType.Int, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, emp));
                            cmd.Parameters.Add(new SqlParameter("@compid", SqlDbType.Int, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, cmp));
                            using (var reader = cmd.ExecuteReader())
                            {
                                var model = this.lstservices.Read(reader).ToList();
                                return View(model);
                            }
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
