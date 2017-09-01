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
    public class SalaryController : Controller
    {
        //
        // GET: /Salary/
        string sqlConnectionString = @"Data Source=KP;Database=VPDPension;Trusted_Connection=true;Persist Security Info=True";
        //string sqlConnectionString = @"data source=TESTSERVERVPD;initial catalog=VPDPension;user id=sa;password=newtech009;MultipleActiveResultSets=True;App=EntityFramework";
        //string sqlConnectionString = @"data source=46.105.241.192,1533;initial catalog=VPDPENSION;user id=pension;password=Newtech@009;MultipleActiveResultSets=True;App=EntityFramework";
        //string sqlConnectionString = @"data source=208.91.198.59;initial catalog=techflow;user id=techflow;password=Newtech@009;MultipleActiveResultSets=True;App=EntityFramework";
        //string sqlConnectionString = @"data source=mydbinstance.c0cgp66jg3yv.ap-southeast-2.rds.amazonaws.com;initial catalog=techflow_online;user id=techflowdbun;password=TFpassw0rd16;MultipleActiveResultSets=True;App=EntityFramework";

        VPDPensionEntities DbContext = new VPDPensionEntities();
        List<SalaryModel> lstSalary = new List<SalaryModel>();
        SalaryModel objModel = new SalaryModel();
        SalaryService objService = new SalaryService();
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
            objModel.SalaryMonth = Convert.ToInt32(System.DateTime.Today.Month);
            lstSalary = objService.getSalary(cid, objModel.Year, Convert.ToInt32(objModel.SalaryMonth));
            objModel.ListSalary = new List<SalaryModel>();
            objModel.ListSalary.AddRange(lstSalary);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(SalaryModel model, HttpPostedFileBase[] file, FormCollection All)
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
                var lstExlClm = DbContext.ColunmMapMasters.Where(m => m.CompID == cid && m.ExlClmName != null).ToList();
                var salaryList = DbContext.SalaryMasters.Where(m => m.CompID == cid && m.Year == model.Year && m.SalaryMonth == model.SalaryMonth).ToList();

                if (model.OverWrite == true)
                {
                    if (salaryList.Count > 0)
                    {
                        foreach (var del in salaryList)
                        {
                            DbContext.SalaryMasters.Remove(DbContext.SalaryMasters.Find(del.SID));
                            DbContext.SaveChanges();
                        }
                    }
                    string strExl = null;

                    foreach (var i in lstExlClm)
                    {
                        strExl += i.ExlClmName + ",";
                        //strExl += "[" + i.ExlClmName + "],";
                    }
                    string ExlClm = strExl.Remove(strExl.Length - 1);
                    string TblClm = strExl.Remove(strExl.Length - 1);
                    if (file != null || Request.Files.Count > 0 || file[0].FileName != null || file[0].ContentLength != null || file[0].InputStream != null)
                    {
                        string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                        string filename = Request.Files["file"].FileName;
                        if (extension != "")
                        {
                            string path1 = string.Format("{0}/{1}", Server.MapPath("~/DataFiles/SalaryData"), Request.Files["file"].FileName);
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

                            OleDbCommand cmd = new OleDbCommand("Select " + @ExlClm + " from [Sheet1$]", excelConnection);

                            excelConnection.Open();
                            string header = null;
                            OleDbDataAdapter objAdapter = new OleDbDataAdapter(cmd);
                            DataTable Dt = new DataTable();
                            objAdapter.Fill(Dt);
                            foreach (DataColumn dc in Dt.Columns)
                            {
                                header += dc.ColumnName + ",";
                            }
                            DataTable dtInsertRows = Dt;
                            string emp = null;
                            for (int i = 0; i < dtInsertRows.Rows.Count; i++)
                            {
                                DataRow row = dtInsertRows.Rows[i];
                                string no = row["EmpNo"].ToString();
                                string name = row["EmpName"].ToString();
                                var empList = DbContext.EmployeeMasters.Where(m => m.CompId == cid && m.EmpNo == no).ToList();

                                if (empList.Count <= 0)
                                {
                                    emp += "(" + no + "," + name + "),";
                                    //dtInsertRows.Rows.Remove(row);
                                    dtInsertRows.Rows[i].Delete();
                                }
                            }
                            dtInsertRows.AcceptChanges();
                            //using (SqlBulkCopy sbc = new SqlBulkCopy(sqlConnectionString, SqlBulkCopyOptions.KeepIdentity))
                            //{
                            //    sbc.DestinationTableName = "SalaryMaster";
                            //    // Number of records to be processed in one go
                            //    sbc.BatchSize = Dt.Rows.Count;

                            //    // Add your column mappings here
                            //    foreach (var c in lstExlClm)
                            //    {
                            //        sbc.ColumnMappings.Add(c.ExlClmName, c.TblClmName);
                            //        //sbc.ColumnMappings.Add("field1", "field3");
                            //        //sbc.ColumnMappings.Add("foo", "bar");
                            //    }
                            //    // Finally write to server
                            //    sbc.WriteToServer(dtInsertRows);
                            //}
                            //OleDbDataReader dReader;
                            //dReader = cmd.ExecuteReader();

                            //SqlBulkCopy sqlBulk = new SqlBulkCopy(sqlConnectionString);
                            ////Give your Destination table name
                            //sqlBulk.DestinationTableName = "SalaryMaster";
                            //excelBook.Close();

                            //sqlBulk.WriteToServer(dReader);
                            excelConnection.Close();
                            //this will remove that issue
                            excelConnection.Dispose();
                            file = null;
                            // SQL Server Connection String

                            //lstSalary = objService.getSalary4Update();
                            //objModel.ListSalary = new List<SalaryModel>();
                            //objModel.ListSalary.AddRange(lstSalary);
                            //foreach (var a in objModel.ListSalary)
                            //{
                            //    string year = model.Year;
                            //    int? month = model.SalaryMonth;

                            //    objService.Update(year, month, cid, uid, a.SID);
                            //}
                            Session["ExcelData"] = Dt;
                            Session["EmpList"] = emp;
                            Session["Month"] = model.SalaryMonth;
                            Session["Year"] = model.Year;
                            //TempData["AMsg"] = filename + " Uploaded Successfully." + "\\nGiven below employee not matched:-\\n" + emp; ;
                        }
                    }
                    return View("ConfirmData");
                }
                else if (salaryList.Count > 0)
                {

                    TempData["AMsg"] = "Data allready exist in database,If you want to overwrite then tick overwrite.";
                    //ViewBag.Msg = "Data allready exist in database,If you want to overwrite then tick overwrite.";
                    //Response.Redirect("Salary/Index");
                    return View("Index");
                }
                else
                {
                    string strExl = null;

                    foreach (var i in lstExlClm)
                    {
                        strExl += i.ExlClmName + ",";
                        //strExl += "[" + i.ExlClmName + "],";
                    }
                    string ExlClm = strExl.Remove(strExl.Length - 1);

                    string strTbl = null;

                    foreach (var i in lstExlClm)
                    {
                        strTbl += i.TblClmName + ",";
                    }
                    string TblClm = strExl.Remove(strExl.Length - 1);
                    if (file != null || Request.Files.Count > 0 || file[0].FileName != null || file[0].ContentLength != null || file[0].InputStream != null)
                    {
                        string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                        string filename = Request.Files["file"].FileName;
                        if (extension != "")
                        {
                            string path1 = string.Format("{0}/{1}", Server.MapPath("~/DataFiles/SalaryData"), Request.Files["file"].FileName);
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

                            OleDbCommand cmd = new OleDbCommand("Select " + @ExlClm + " from [Sheet1$]", excelConnection);

                            excelConnection.Open();
                            string header = null;
                            OleDbDataAdapter objAdapter = new OleDbDataAdapter(cmd);
                            DataTable Dt = new DataTable();
                            objAdapter.Fill(Dt);
                            foreach (DataColumn dc in Dt.Columns)
                            {
                                header += dc.ColumnName + ",";
                            }

                            DataTable dtInsertRows = Dt;
                            string emp = null;
                           
                            for (int i = 0; i < dtInsertRows.Rows.Count; i++)
                            {
                                DataRow row = dtInsertRows.Rows[i];
                                string no = row["EmpNo"].ToString();
                                string name = row["EmpName"].ToString();
                                var empList = DbContext.EmployeeMasters.Where(m => m.CompId == cid && m.EmpNo == no).ToList();

                                if (empList.Count <= 0)
                                {
                                    emp += no + "/" + name+",";
                                    //dtInsertRows.Rows.Remove(row);
                                    dtInsertRows.Rows[i].Delete();

                                }

                            }
                            dtInsertRows.AcceptChanges();
                            //using (SqlBulkCopy sbc = new SqlBulkCopy(sqlConnectionString, SqlBulkCopyOptions.KeepIdentity))
                            //{
                            //    sbc.DestinationTableName = "SalaryMaster";

                            //    // Number of records to be processed in one go
                            //    sbc.BatchSize = Dt.Rows.Count;

                            //    // Add your column mappings here
                            //    foreach (var c in lstExlClm)
                            //    {
                            //        sbc.ColumnMappings.Add(c.ExlClmName, c.TblClmName);

                            //        //sbc.ColumnMappings.Add("field1", "field3");
                            //        //sbc.ColumnMappings.Add("foo", "bar");
                            //    }
                            //    // Finally write to server
                            //    sbc.WriteToServer(dtInsertRows);
                            //}




                            //OleDbDataReader dReader;
                            //dReader = cmd.ExecuteReader();

                            //SqlBulkCopy sqlBulk = new SqlBulkCopy(sqlConnectionString);
                            ////Give your Destination table name
                            //sqlBulk.DestinationTableName = "SalaryMaster";
                            //excelBook.Close();

                            //sqlBulk.WriteToServer(dReader);
                            excelConnection.Close();
                            //this will remove that issue

                            excelConnection.Dispose();



                            file = null;

                            // SQL Server Connection String




                            //lstSalary = objService.getSalary4Update();
                            //objModel.ListSalary = new List<SalaryModel>();
                            //objModel.ListSalary.AddRange(lstSalary);
                            //foreach (var a in objModel.ListSalary)
                            //{
                            //    string year = model.Year;
                            //    int? month = model.SalaryMonth;

                            //    objService.Update(year, month, cid, uid, a.SID);
                            //}
                            Session["ExcelData"] = Dt;
                            Session["EmpList"] = emp;
                            Session["Month"] = model.SalaryMonth;
                            Session["Year"] = model.Year;

                            string emp1 = Session["EmpList"].ToString();
                            var emp11 = emp.Split(',').ToList();
                            
                            List<InvalidEmpModel> lstInvalidEmp = new List<InvalidEmpModel>();
                            foreach (string strDropdown in emp11)
                            {
                                if (strDropdown != "" && strDropdown!="/")
                                {
                                    var no = strDropdown.Split('/');
                                    lstInvalidEmp.Add(new InvalidEmpModel() { No = Convert.ToInt32(no[0]), Name = no[1] });
                                }
                            }
                            ViewBag.EmpList = lstInvalidEmp;  
                            //TempData["AMsg"] = filename + " Uploaded Successfully." + "\\nGiven below employee not matched:-\\n" + emp;
                        }
                    }
                    return View("ConfirmData");
                }
            }
            catch (Exception ex)
            {
                TempData["AMsg"] = "Error!" + ex;
                ErrorLogClass.WriteErrorLog("AdminManagement", "SalaryMaster", "SheetUpload", ex);
                return View("Index");
            }
            
        }
        public class InvalidEmpModel
        {
            public int No { get; set; }
            public string Name { get; set; }
        }
        public ActionResult ConfirmData()
        {
                               
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmData(SalaryModel model)
        {
            int cid = 0;
            int uid = 0;
            if (Session["Comp"] != null)
            {
                cid = Convert.ToInt32(Session["Comp"].ToString());
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            DataTable dtInsertRows = (DataTable)Session["ExcelData"];
            var lstExlClm = DbContext.ColunmMapMasters.Where(m => m.CompID == cid && m.ExlClmName != null).ToList();
            
            
             

            using (SqlBulkCopy sbc = new SqlBulkCopy(sqlConnectionString, SqlBulkCopyOptions.KeepIdentity))
            {
                sbc.DestinationTableName = "SalaryMaster";

                // Number of records to be processed in one go
                sbc.BatchSize = dtInsertRows.Rows.Count;

                // Add your column mappings here
                foreach (var c in lstExlClm)
                {
                    sbc.ColumnMappings.Add(c.ExlClmName, c.TblClmName);

                    //sbc.ColumnMappings.Add("field1", "field3");
                    //sbc.ColumnMappings.Add("foo", "bar");
                }
                // Finally write to server
                sbc.WriteToServer(dtInsertRows);
            }

            lstSalary = objService.getSalary4Update();
            objModel.ListSalary = new List<SalaryModel>();
            objModel.ListSalary.AddRange(lstSalary);
            foreach (var a in objModel.ListSalary)
            {
                string year = Session["Year"].ToString();
                int? month = Convert.ToInt32(Session["Month"].ToString());

                objService.Update(year, month, cid, uid, a.SID);
            }
            TempData["AMsg"] = "Data Uploaded Successfully.";
            return RedirectToAction("Index");
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
            lstSalary = objService.getSalByTime(year, month, cid);
            objModel.ListSalary = new List<SalaryModel>();
            objModel.ListSalary.AddRange(lstSalary);
            return PartialView("_Data", objModel.ListSalary);
        }
    }
}
