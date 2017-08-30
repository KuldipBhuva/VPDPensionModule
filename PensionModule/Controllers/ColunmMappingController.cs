using PensionModel;
using PensionModel.ViewModel;
using PensionServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PensionModule.Controllers
{
    public class ColunmMappingController : Controller
    {
        //
        // GET: /ColunmMapping/
        //string sqlConnectionString = @"Data Source=KP;Database=VPDPension;Trusted_Connection=true;Persist Security Info=True";
        string sqlConnectionString = @"data source=TESTSERVERVPD;initial catalog=VPDPension;user id=sa;password=newtech009;MultipleActiveResultSets=True;App=EntityFramework";
        //string sqlConnectionString = @"data source=46.105.241.192,1533;initial catalog=VPDPENSION;user id=pension;password=Newtech@009;MultipleActiveResultSets=True;App=EntityFramework";
        //string sqlConnectionString = @"data source=208.91.198.59;initial catalog=techflow;user id=techflow;password=Newtech@009;MultipleActiveResultSets=True;App=EntityFramework";
        //string sqlConnectionString = @"data source=mydbinstance.c0cgp66jg3yv.ap-southeast-2.rds.amazonaws.com;initial catalog=techflow_online;user id=techflowdbun;password=TFpassw0rd16;MultipleActiveResultSets=True;App=EntityFramework";

        VPDPensionEntities DbContext = new VPDPensionEntities();
        ColunmMappingService objService = new ColunmMappingService();
        List<ColunmMapModel> lstpen = new List<ColunmMapModel>();
        ColunmMapModel objModel = new ColunmMapModel();
        public ActionResult Index()
        {
            return View();
        }

        public class DropdownModel
        {
            public int Index { get; set; }
            public string Value { get; set; }
        }
        [HttpPost]
        public ActionResult Index(ColunmMapModel model, HttpPostedFileBase[] file,List<HttpPostedFileBase> filedata)
        {
            if (file != null || Request.Files.Count > 0 || file[0].FileName != null || file[0].ContentLength != null || file[0].InputStream != null)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                string filename = Request.Files["file"].FileName;
                if (extension != "")
                {
                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/DataFiles/ColumnMapData"), Request.Files["file"].FileName);
                    //FileStream s = new FileStream(path1, FileMode.Open);
                    if (System.IO.File.Exists(path1))
                    {
                        System.IO.File.Delete(path1);
                    }
                    Request.Files["file"].SaveAs(path1);
                    
                    //Create connection string to Excel work book
                    string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                    //string excelConnectionString = @"Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + path1 + ";Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                    //Create Connection to Excel work book
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    ////Create OleDbCommand to fetch data from Excel
                    //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                    ////Microsoft.Office.Interop.Excel.Application xlApp;
                    
                    ////get the workbook
                    //Microsoft.Office.Interop.Excel.Workbook excelBook = xlApp.Workbooks.Open(path1, Notify: false, ReadOnly: true);

                    ////get the first worksheet
                    //Microsoft.Office.Interop.Excel.Worksheet wSheet = excelBook.Sheets.Item[1];

                    OleDbCommand cmd = new OleDbCommand("Select * from [Sheet1$]", excelConnection);
                    
                    excelConnection.Open();
                    string header = null;
                    string index=null;
                    OleDbDataAdapter objAdapter = new OleDbDataAdapter(cmd);
                    DataTable Dt = new DataTable();
                    objAdapter.Fill(Dt);
                   
                    
                    foreach (DataColumn dc in Dt.Columns)
                    {
                        header += dc.ColumnName + ",";
                        index += dc.Ordinal + ",";
                    }
                    string strh = header.Remove(header.Length - 1);
                    string stri = index.Remove(index.Length - 1);
                    var names = strh.Split(',').ToList();
                  
                    List<DropdownModel> lstDropdown = new List<DropdownModel>();
                    foreach (string strDropdown in names)
                    {
                        lstDropdown.Add(new DropdownModel() { Index = names.IndexOf(strDropdown), Value = strDropdown });
                    }

                    
                    ViewBag.Header = lstDropdown;
                    excelConnection.Close();
                    excelConnection.Dispose();
                    //excelBook.Close();
                }
            }
            
            List<ColunmMapModel> lstClm = new List<ColunmMapModel>();
            lstClm = objService.getColumn(model.TblName);
            objModel.ListClm = new List<ColunmMapModel>();
            objModel.ListClm.AddRange(lstClm);
            return PartialView("_Clm",objModel);
        }

        public ActionResult getColunm(string table, ColunmMapModel model, List<HttpPostedFileBase> file)
        {
            if (file != null || Request.Files.Count > 0 || file[0].FileName != null || file[0].ContentLength != null || file[0].InputStream != null)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                string filename = Request.Files["file"].FileName;
                if (extension != "")
                {
                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/DataFiles/ColumnMapData"), Request.Files["file"].FileName);
                    //FileStream s = new FileStream(path1, FileMode.Open);
                    if (System.IO.File.Exists(path1))
                    {
                        System.IO.File.Delete(path1);
                    }
                    Request.Files["file"].SaveAs(path1);
                   
                    //Create connection string to Excel work book
                    string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                    //string excelConnectionString = @"Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};DBQ=" + path1 + ";Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';Persist Security Info=False";
                    //Create Connection to Excel work book
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    ////Create OleDbCommand to fetch data from Excel
                    //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                    ////get the workbook
                    //Microsoft.Office.Interop.Excel.Workbook excelBook = xlApp.Workbooks.Open(path1, Notify: false, ReadOnly: true);

                    ////get the first worksheet
                    //Microsoft.Office.Interop.Excel.Worksheet wSheet = excelBook.Sheets.Item[1];

                    OleDbCommand cmd = new OleDbCommand("Select * from [Sheet1$]", excelConnection);

                    excelConnection.Open();
                    string header = null;
                    string index = null;
                    OleDbDataAdapter objAdapter = new OleDbDataAdapter(cmd);
                    DataTable Dt = new DataTable();
                    objAdapter.Fill(Dt);


                    foreach (DataColumn dc in Dt.Columns)
                    {
                        header += dc.ColumnName + ",";
                        index += dc.Ordinal + ",";
                    }
                    string strh = header.Remove(header.Length - 1);
                    string stri = index.Remove(index.Length - 1);
                    var names = strh.Split(',').ToList();

                    List<DropdownModel> lstDropdown = new List<DropdownModel>();
                    foreach (string strDropdown in names)
                    {
                        lstDropdown.Add(new DropdownModel() { Index = names.IndexOf(strDropdown), Value = strDropdown });
                    }


                    ViewBag.Header = lstDropdown;
                    excelConnection.Close();
                    excelConnection.Dispose();
                }
                
            }
         

            List<ColunmMapModel> lstClm = new List<ColunmMapModel>();
            lstClm = objService.getColumn(model.TblName);
            objModel.ListClm = new List<ColunmMapModel>();
            objModel.ListClm.AddRange(lstClm);
            return PartialView("_Clm", objModel);
        }
        public ActionResult InsertColunm(ColunmMapModel model)
        {
            int cid = 0;
            int uid = 0;
            if (Session["Comp"] != null)
            {
                cid = Convert.ToInt32(Session["Comp"].ToString());
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            var lst = DbContext.ColunmMapMasters.Where(m => m.CompID == cid && m.TblName == model.TblName).ToList();
            if (model.OverWrite == true)
            {
                if (lst.Count > 0)
                    {
                        foreach (var del in lst)
                        {
                            DbContext.ColunmMapMasters.Remove(DbContext.ColunmMapMasters.Find(del.CMID));
                            DbContext.SaveChanges();
                        }
                    }
                foreach (var i in model.ListClm)
                {
                    int compid = cid;
                    string tbl = model.TblName;
                    string tblClm = i.COLUMN_NAME;
                    int tblIndex = Convert.ToInt32(i.ORDINAL_POSITION);
                    string exlClm = i.ExlClmName;
                    int mapBy = uid;
                    DateTime mapDate = System.DateTime.Now;
                    objService.Insert(compid, tbl, tblClm, tblIndex, exlClm, mapBy, mapDate);
                }
            }
            else if (lst.Count > 0)
            {
                TempData["AMsg"] = "Data allready exist in database,If you want to overwrite then tick overwrite.";
            }
            else
            {
                foreach (var i in model.ListClm)
                {
                    int compid = cid;
                    string tbl = model.TblName;
                    string tblClm = i.COLUMN_NAME;
                    int tblIndex = Convert.ToInt32(i.ORDINAL_POSITION);
                    string exlClm = i.ExlClmName;
                    int mapBy = uid;
                    DateTime mapDate = System.DateTime.Now;
                    objService.Insert(compid, tbl, tblClm, tblIndex, exlClm, mapBy, mapDate);
                }
            }
            return View("Index");            
        }
    }

}
