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
    public class PensionController : Controller
    {
        //
        // GET: /Pension/
        //string sqlConnectionString = @"Data Source=KP;Database=VPDPension;Trusted_Connection=true;Persist Security Info=True";
        string sqlConnectionString = @"data source=TESTSERVERVPD;initial catalog=VPDPension;user id=sa;password=newtech009;MultipleActiveResultSets=True;App=EntityFramework";
        //string sqlConnectionString = @"data source=208.91.198.59;initial catalog=techflow;user id=techflow;password=Newtech@009;MultipleActiveResultSets=True;App=EntityFramework";
        //string sqlConnectionString = @"data source=mydbinstance.c0cgp66jg3yv.ap-southeast-2.rds.amazonaws.com;initial catalog=techflow_online;user id=techflowdbun;password=TFpassw0rd16;MultipleActiveResultSets=True;App=EntityFramework";
                //string sqlConnectionString = @"data source=46.105.241.192,1533;initial catalog=VPDPENSION;user id=pension;password=Newtech@009;MultipleActiveResultSets=True;App=EntityFramework";
        VPDPensionEntities DbContext = new VPDPensionEntities();        
        PensionMasterService objService = new PensionMasterService();
        List<PensionMasterModel> lstpen = new List<PensionMasterModel>();
        PensionMasterModel objModel = new PensionMasterModel();
        public ActionResult Index()
        {
            int cid = 0;
            int uid = 0;
            if (Session["Comp"] != null)
            {
                cid = Convert.ToInt32(Session["Comp"].ToString());
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            lstpen = objService.getPensionByComp(cid);
            objModel.ListPension = new List<PensionMasterModel>();
            objModel.ListPension.AddRange(lstpen);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(PensionMasterModel model, HttpPostedFileBase[] file, FormCollection All)
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
                        string path1 = string.Format("{0}/{1}", Server.MapPath("~/DataFiles/PensionData"), Request.Files["file"].FileName);
                        if (System.IO.File.Exists(path1))
                            System.IO.File.Delete(path1);

                        Request.Files["file"].SaveAs(path1);
                        
                        //Create connection string to Excel work book
                        string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                        //string excelConnectionString = @"Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + path1 + ";Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                        //Create Connection to Excel work book
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        //Create OleDbCommand to fetch data from Excel

                        OleDbCommand cmd = new OleDbCommand("Select [PID],[emp_name],[policy_no] ,[annuity_no],[gross_amt],[it_amt],[cheque_amt] from [Sheet1$]", excelConnection);

                        excelConnection.Open();
                        OleDbDataReader dReader;
                        dReader = cmd.ExecuteReader();

                        SqlBulkCopy sqlBulk = new SqlBulkCopy(sqlConnectionString);
                        //Give your Destination table name
                        sqlBulk.DestinationTableName = "PensionMaster";
                        sqlBulk.WriteToServer(dReader);
                        excelConnection.Close();
                        file = null;

                        // SQL Server Connection String




                        lstpen = objService.getPension(cid);
                        objModel.ListPension = new List<PensionMasterModel>();
                        objModel.ListPension.AddRange(lstpen);
                        foreach (var a in objModel.ListPension)
                        {
                            string year = model.Year;
                            string month = model.PensionMonth;

                            objService.Update(year, month, a.PID, uid);
                        }
                        TempData["AMsg"] = filename + " Uploaded Successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "PensionMaster", "SheetUpload", ex);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Process()
        {
            try
            {
                List<PaymentTypeModel> lstPayType = new List<PaymentTypeModel>();
                lstPayType = objService.getActivePayType();
                objModel.ListPayType = new List<PaymentTypeModel>();
                objModel.ListPayType.AddRange(lstPayType);
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "PensionProcess", "Process", ex);
            }
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Process(PensionMasterModel model)
        {
            try
            {
                foreach (var i in model.ListPension)
                {
                    //var lst = DbContext.PensionMasters.Where(m => m.PID == i.PID).SingleOrDefault();
                    //if (lst.IsProcessed == null)
                    //{
                    //    if (i.IsProcessed == true)
                    //    {
                    PensionMaster pm = DbContext.PensionMasters.Where(u => u.PID == i.PID).SingleOrDefault();
                    pm.IsProcessed = i.IsProcessed;
                    if (i.IsProcessed == false)
                    {
                        pm.ProcessDate = null;
                    }
                    else
                    {
                        pm.ProcessDate = System.DateTime.Now;
                    }
                    DbContext.SaveChanges();
                    //    }
                    //}
                    //else
                    //{
                    //    PensionMaster pm = DbContext.PensionMasters.Where(u => u.PID == i.PID).SingleOrDefault();
                    //    pm.IsProcessed = i.IsProcessed;
                    //    pm.ProcessDate = System.DateTime.Now;
                    //    DbContext.SaveChanges();
                    //}
                }
                TempData["AMsg"] = "Data Updated Successfully.";
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "PensionProcess", "UpdateProcess", ex);
            }

            return RedirectToAction("Process");
        }
        public ActionResult getPension(string year, string month, int pay)
        {

            List<PensionMasterModel> lstPension = new List<PensionMasterModel>();

            int cid = 0;
            int uid = 0;
            if (Session["Comp"] != null || Session["Comp"].ToString() != "0")
            {
                cid = Convert.ToInt32(Session["Comp"].ToString());
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            lstPension = objService.getPensionByMonth(year, month, cid, pay);
            objModel.ListPension = new List<PensionMasterModel>();
            objModel.ListPension.AddRange(lstPension);
            return PartialView("_PensionData", objModel);


            //return Json(objModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Arrears(int id, string m, string y)
        {
            List<PensionMasterModel> lstPension = new List<PensionMasterModel>();
            string anuity = Convert.ToString(id);
            lstPension = objService.getPensionByANo(anuity, m, y);
            objModel.ListPension = new List<PensionMasterModel>();
            objModel.ListPension.AddRange(lstPension);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Arrears(PensionMasterModel model)
        {
            try
            {
                foreach (var i in model.ListPension)
                {
                    //var lst = DbContext.PensionMasters.Where(m => m.PID == i.PID).SingleOrDefault();
                    //if (lst.IsProcessed == null)
                    //{
                    //    if (i.IsProcessed == true)
                    //    {
                    PensionMaster pm = DbContext.PensionMasters.Where(u => u.PID == i.PID).SingleOrDefault();
                    pm.IsProcessed = true;
                    pm.ProcessDate = null;
                    pm.SettelmentDate = System.DateTime.Now;

                    DbContext.SaveChanges();
                    //    }
                    //}
                    //else
                    //{
                    //    PensionMaster pm = DbContext.PensionMasters.Where(u => u.PID == i.PID).SingleOrDefault();
                    //    pm.IsProcessed = i.IsProcessed;
                    //    pm.ProcessDate = System.DateTime.Now;
                    //    DbContext.SaveChanges();
                    //}
                }
                TempData["AMsg"] = "Data Updated Successfully.";
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "PensionProcess", "UpdateArrears", ex);
            }

            return RedirectToAction("Process");
        }
    }
}
