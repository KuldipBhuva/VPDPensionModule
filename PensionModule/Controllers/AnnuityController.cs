using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pension.Models.ViewModel;
using Pension.Services;
using PensionModel.ViewModel;
using PensionServices;

namespace PensionModule.Controllers
{
    public class AnnuityController : Controller
    {
        AnnuityViewModel lstmodel = new AnnuityViewModel();
        AnnuityServices lstservices = new AnnuityServices();
        public ActionResult Annuity()
        {
            BindIns();
            lstmodel.ListIns = ViewBag.ListIns;
            int id = Convert.ToInt32(Session["Emp"]);
            lstmodel.lstaty = lstservices.lstdata(id);
            lstmodel.LICId = 0;
            BindPlan(lstmodel);
            return View(lstmodel);
            //return View();
        }

        [HttpPost]
        public ActionResult Annuity(AnnuityViewModel model, HttpPostedFileBase[] file, FormCollection All)
        {
            try
            {
                int cid = 0;
                int uid = 0;
                if (Session["Comp"] != null)
                {
                    cid = Convert.ToInt32(Session["Comp"].ToString());
                    uid = Convert.ToInt32(Session["UID"].ToString());
                }
                if (file != null || Request.Files.Count > 0 || file[0].FileName != null || file[0].ContentLength != null || file[0].InputStream != null)
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                    string filename = Request.Files["file"].FileName;
                    if (extension != "")
                    {
                        string path1 = string.Format("{0}/{1}", Server.MapPath("~/DataFiles/AnnuityData/"), Request.Files["file"].FileName);
                        if (System.IO.File.Exists(path1))
                            System.IO.File.Delete(path1);

                        Request.Files["file"].SaveAs(path1);
                        //string sqlConnectionString = @"Data Source=KP;Database=VPDPension;Trusted_Connection=true;Persist Security Info=True";
                        string sqlConnectionString = @"data source=TESTSERVERVPD;initial catalog=VPDPension;user id=sa;password=newtech009;MultipleActiveResultSets=True;App=EntityFramework";
                        //string sqlConnectionString = @"data source=46.105.241.192,1533;initial catalog=VPDPENSION;user id=pension;password=Newtech@009;MultipleActiveResultSets=True;App=EntityFramework";
                        //string sqlConnectionString = @"data source=208.91.198.59;initial catalog=techflow;user id=techflow;password=Newtech@009;MultipleActiveResultSets=True;App=EntityFramework";
                        //string sqlConnectionString = @"data source=mydbinstance.c0cgp66jg3yv.ap-southeast-2.rds.amazonaws.com;initial catalog=techflow_online;user id=techflowdbun;password=TFpassw0rd16;MultipleActiveResultSets=True;App=EntityFramework";
                        //Create connection string to Excel work book
                        string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                        //string excelConnectionString = @"Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + path1 + ";Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                        //Create Connection to Excel work book
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        //Create OleDbCommand to fetch data from Excel

                        OleDbCommand cmd = new OleDbCommand("Select [ID],[AGE],[YLY],[HLY],[QLY],[MLY] from [Sheet1$]", excelConnection);

                        excelConnection.Open();
                        OleDbDataReader dReader;
                        dReader = cmd.ExecuteReader();

                        SqlBulkCopy sqlBulk = new SqlBulkCopy(sqlConnectionString);
                        //Give your Destination table name
                        sqlBulk.DestinationTableName = "Annuity";
                        sqlBulk.WriteToServer(dReader);
                        excelConnection.Close();
                        file = null;
                        try
                        {
                            //SQL Server Connection String
                            List<AnnuityViewModel> listaty = new List<AnnuityViewModel>();
                            listaty = lstservices.getannuity();
                            lstmodel.listcheck = new List<AnnuityViewModel>();
                            lstmodel.listcheck.AddRange(listaty);
                            foreach (var a in lstmodel.listcheck)
                            {
                                int licid = Convert.ToInt32(model.LICId);
                                int planid = Convert.ToInt32(model.PlanID);
                                DateTime effdate = Convert.ToDateTime(model.EffDate);
                                int id = Convert.ToInt32(a.Id);
                                lstservices.Update(licid, planid, effdate, id);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        TempData["AMsg"] = filename + " Uploaded Successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "Annuity", "SheetUpload", ex);
            }
            return RedirectToAction("Annuity");
        }

        public JsonResult BindPlan(AnnuityViewModel model)
        {
            PlanServices insservices = new PlanServices();
            List<PlanViewModel> lstplan = new List<PlanViewModel>();
            int id = Convert.ToInt32(model.LICId);
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

    }
}
