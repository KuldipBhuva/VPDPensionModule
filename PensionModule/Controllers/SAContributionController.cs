using Pension.Models.ViewModel;
using PensionModel;
using PensionModel.ViewModel;
using PensionServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PensionModule.Controllers
{
    public class SAContributionController : Controller
    {
        //
        // GET: /SAContribution/
        //string sqlConnectionString = @"data source=TESTSERVERVPD;initial catalog=VPDPension;user id =sa;password=newtech009;MultipleActiveResultSets=True;App=EntityFramework";
        string sqlConnectionString = @"Data Source=KP;Database=VPDPension;Trusted_Connection=true;Persist Security Info=True";
        VPDPensionEntities DbContext = new VPDPensionEntities();
        List<SAContributionModel> lstSA = new List<SAContributionModel>();
        SAContributionModel objModel = new SAContributionModel();
        SAService objService = new SAService();
        public ActionResult Index()
        {
            int cid = 0;
            int uid = 0;
            if (Session["Comp"] != null)
            {
                cid = Convert.ToInt32(Session["Comp"].ToString());
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            objModel.Year = Convert.ToString(System.DateTime.Today.Year);
            objModel.Month = System.DateTime.Today.Month;
            lstSA = objService.getSA(cid, objModel.Year,objModel.Month);
            objModel.ListSA = new List<SAContributionModel>();
            objModel.ListSA.AddRange(lstSA);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(SAContributionModel model, HttpPostedFileBase[] file)
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
                var Comp = DbContext.CompanyMasters.Where(m => m.id == cid).SingleOrDefault();               
                var SAList = DbContext.SAContributions.Where(m => m.CompID == cid && m.Year == model.Year && m.Month == model.Month).ToList();

                if (model.OverWrite == true)
                {
                    if (SAList.Count > 0)
                    {
                        foreach (var del in SAList)
                        {
                            DbContext.SAContributions.Remove(DbContext.SAContributions.Find(del.SAID));
                            DbContext.SaveChanges();
                        }
                    }                   
                    
                    if (file != null || Request.Files.Count > 0 || file[0].FileName != null || file[0].ContentLength != null || file[0].InputStream != null)
                    {
                        string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                        string filename = Request.Files["file"].FileName;
                        if (extension != "")
                        {
                            string path1 = string.Format("{0}/{1}", Server.MapPath("~/DataFiles/SAData"), Request.Files["file"].FileName);
                            if (System.IO.File.Exists(path1))
                                System.IO.File.Delete(path1);

                            Request.Files["file"].SaveAs(path1);


                            //Create connection string to Excel work book
                            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                            //string excelConnectionString = @"Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + path1 + ";Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                            //Create Connection to Excel work book
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                            ////get the workbook
                            //Microsoft.Office.Interop.Excel.Workbook excelBook = xlApp.Workbooks.Open(path1, Notify: false, ReadOnly: true);

                            ////get the first worksheet
                            //Microsoft.Office.Interop.Excel.Worksheet wSheet = excelBook.Sheets.Item[1];
                            ////Create OleDbCommand to fetch data from Excel

                            OleDbCommand cmd = new OleDbCommand("Select SAID,emp_no,name,sa_basic,sa_contribution,emp_contribution from [Sheet1$]", excelConnection);

                            excelConnection.Open();
                            //string header = null;
                            OleDbDataAdapter objAdapter = new OleDbDataAdapter(cmd);
                            DataTable Dt = new DataTable();
                            objAdapter.Fill(Dt);
                            //foreach (DataColumn dc in Dt.Columns)
                            //{
                            //    header += dc.ColumnName + ",";
                            //}
                            DataTable dtInsertRows = Dt;
                            string emp = null;
                            for (int i = 0; i < dtInsertRows.Rows.Count; i++)
                            {
                                DataRow row = dtInsertRows.Rows[i];
                                string no = row["emp_no"].ToString();
                                string name = row["name"].ToString();
                                var empList = DbContext.EmployeeMasters.Where(m => m.CompId == cid && m.EmpNo == no).ToList();

                                if (empList.Count <= 0)
                                {
                                    //emp += "(" + no + "," + name + ")\n,";
                                    //emp += no + "," + name + "\n";
                                    emp += no + "  ,   " + name + "<br/>";
                                    //dtInsertRows.Rows.Remove(row);
                                    dtInsertRows.Rows[i].Delete();
                                }
                            }
                            dtInsertRows.AcceptChanges();



                            //OleDbDataReader dReader;
                            //dReader = cmd.ExecuteReader();

                            SqlBulkCopy sqlBulk = new SqlBulkCopy(sqlConnectionString);
                            //Give your Destination table name
                            sqlBulk.DestinationTableName = "SAContribution";
                            sqlBulk.WriteToServer(dtInsertRows);
                            excelConnection.Close();
                            file = null;

                            lstSA = objService.getSA4Update();
                            objModel.ListSA = new List<SAContributionModel>();
                            objModel.ListSA.AddRange(lstSA);
                            foreach (var a in objModel.ListSA)
                            {
                                string year = model.Year;
                                int? month = model.Month;

                                objService.Update(year, month, cid, uid, a.SAID);
                            }
                            //TempData["AMsg"] = filename + " Uploaded Successfully.\\nBelow employee(s)are invalid :-\\n" + emp;                            
                            if (emp == null)
                            {
                                ViewBag.Message = filename + " Uploaded Successfully.";
                            }
                            else
                            {
                                ViewBag.Message = filename + " Uploaded Successfully." + "<br/><b>Below employee(s)are invalid :-</b><br/>" + emp;
                            }
                        }
                    }
                    
                }
                else if (SAList.Count > 0)
                {

                    TempData["AMsg"] = "Data allready exist in database,If you want to overwrite then tick overwrite.";
                    //ViewBag.Msg = "Data allready exist in database,If you want to overwrite then tick overwrite.";
                    //Response.Redirect("Salary/Index");
                    //return View("Index");
                }
                else
                {                    
                    if (file != null || Request.Files.Count > 0 || file[0].FileName != null || file[0].ContentLength != null || file[0].InputStream != null)
                    {
                        string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                        string filename = Request.Files["file"].FileName;
                        if (extension != "")
                        {
                            string path1 = string.Format("{0}/{1}", Server.MapPath("~/DataFiles/SAData"), Request.Files["file"].FileName);
                            if (System.IO.File.Exists(path1))
                                System.IO.File.Delete(path1);

                            Request.Files["file"].SaveAs(path1);

                            //Create connection string to Excel work book
                            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                            //string excelConnectionString = @"Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + path1 + ";Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                            //Create Connection to Excel work book
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                            ////get the workbook
                            //Microsoft.Office.Interop.Excel.Workbook excelBook = xlApp.Workbooks.Open(path1, Notify: false, ReadOnly: true);

                            ////get the first worksheet
                            //Microsoft.Office.Interop.Excel.Worksheet wSheet = excelBook.Sheets.Item[1];
                            ////Create OleDbCommand to fetch data from Excel

                            OleDbCommand cmd = new OleDbCommand("Select SAID,emp_no,name,sa_basic,sa_contribution,emp_contribution from [Sheet1$]", excelConnection);

                            excelConnection.Open();
                            //string header = null;
                            OleDbDataAdapter objAdapter = new OleDbDataAdapter(cmd);
                            DataTable Dt = new DataTable();
                            objAdapter.Fill(Dt);
                            //foreach (DataColumn dc in Dt.Columns)
                            //{
                            //    header += dc.ColumnName + ",";
                            //}
                            DataTable dtInsertRows = Dt;
                            string emp = null;
                            for (int i = 0; i < dtInsertRows.Rows.Count; i++)
                            {
                                DataRow row = dtInsertRows.Rows[i];
                                string no = row["emp_no"].ToString();
                                string name = row["name"].ToString();
                                var empList = DbContext.EmployeeMasters.Where(m => m.CompId == cid && m.EmpNo == no).ToList();

                                if (empList.Count <= 0)
                                {
                                    //emp += "(" + no + "," + name + ")\n,";
                                    //emp += no + "," + name + "\n";
                                    emp += no + "  ,   " + name + "<br/>";
                                    //dtInsertRows.Rows.Remove(row);
                                    dtInsertRows.Rows[i].Delete();
                                }
                            }
                            dtInsertRows.AcceptChanges();



                            //OleDbDataReader dReader;
                            //dReader = cmd.ExecuteReader();

                            SqlBulkCopy sqlBulk = new SqlBulkCopy(sqlConnectionString);
                            //Give your Destination table name
                            sqlBulk.DestinationTableName = "SAContribution";
                            sqlBulk.WriteToServer(dtInsertRows);
                            excelConnection.Close();
                            file = null;

                            lstSA = objService.getSA4Update();
                            objModel.ListSA = new List<SAContributionModel>();
                            objModel.ListSA.AddRange(lstSA);
                            foreach (var a in objModel.ListSA)
                            {
                                string year = model.Year;
                                int? month = model.Month;

                                objService.Update(year, month, cid, uid, a.SAID);
                            }
                            //TempData["AMsg"] = filename + " Uploaded Successfully.\\nBelow employee(s)are invalid :-\\n" + emp;                            
                            if (emp == null)
                            {
                                ViewBag.Message = filename + " Uploaded Successfully.";
                            }
                            else
                            {
                                ViewBag.Message = filename + " Uploaded Successfully." + "<br/><b>Below employee(s)are invalid :-</b><br/>" + emp;
                            }
                        }
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                TempData["AMsg"] = "Error!" + ex;
                ErrorLogClass.WriteErrorLog("AdminManagement", "SAMaster", "SheetUpload", ex);
                return View("Index");
            }
            return View("Index");
        }
        public ActionResult getData(string year, int month)
        {
            int cid = 0;
            int uid = 0;
            if (Session["Comp"] != null)
            {
                cid = Convert.ToInt32(Session["Comp"].ToString());
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            lstSA = objService.getSA(cid,year, month);
            objModel.ListSA = new List<SAContributionModel>();
            objModel.ListSA.AddRange(lstSA);
            return PartialView("_SAData", objModel.ListSA);
        }
        public ActionResult Edit(int id)
        {


            objModel = objService.GetById(id);
        
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Edit(SAContributionModel model)
        {
            int cid = 0;
            int uid = 0;
            if (Session["Comp"] != null)
            {
                cid = Convert.ToInt32(Session["Comp"].ToString());
                uid = Convert.ToInt32(Session["UID"].ToString());
            }                      
            objService.Update(model);
            return RedirectToAction("Index");
        }
    }
}
