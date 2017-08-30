using AutoMapper;
using Pension.Models.ViewModel;
using PensionModel;
using PensionModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionServices
{
    public class SalaryService
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        public List<SalaryModel> getSalary(int cid, string year, int Month)
        {
            try
            {
                Mapper.CreateMap<SalaryMaster, SalaryModel>();
                List<SalaryMaster> tblMaster = DbContext.SalaryMasters.Where(m => m.CompID == cid && m.Year == year && m.SalaryMonth == Month).ToList();
                List<SalaryModel> lstmasterdata = Mapper.Map<List<SalaryModel>>(tblMaster);
                return lstmasterdata;


            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("SalaryMaster", "SalaryMasterService", "getSalary", ex);
                return null;
            }


        }
        public List<SalaryModel> getSalByTime(string year, int month, int cid)
        {
            try
            {
                Mapper.CreateMap<SalaryMaster, SalaryModel>();
                List<SalaryMaster> tblMaster = DbContext.SalaryMasters.Where(m => m.CompID == cid && m.Year == year && m.SalaryMonth == month).ToList();
                List<SalaryModel> lstmasterdata = Mapper.Map<List<SalaryModel>>(tblMaster);
                return lstmasterdata;


            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("SalaryMaster", "SalaryMasterService", "getSalary", ex);
                return null;
            }


        }
        public List<SalaryModel> getSalary4Update()
        {
            try
            {
                Mapper.CreateMap<SalaryMaster, SalaryModel>();
                List<SalaryMaster> tblMaster = DbContext.SalaryMasters.Where(m => m.Year == null).ToList();
                List<SalaryModel> lstmasterdata = Mapper.Map<List<SalaryModel>>(tblMaster);
                return lstmasterdata;


            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("SalaryMaster", "SalaryMasterService", "getSalary", ex);
                return null;
            }


        }
        public int Update(string year, int? month, int cid, int uid, int sid)
        {
            try
            {
                SalaryMaster objAllo = new SalaryMaster();
                objAllo = DbContext.SalaryMasters.SingleOrDefault(m => m.SID == sid);
                objAllo.Year = year;
                objAllo.SalaryMonth = month;
                objAllo.CompID = cid;
                objAllo.UploadedBy = uid;
                objAllo.UploadedDate = System.DateTime.Now;
                return DbContext.SaveChanges();

            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "SalaryService", "Update", ex);
                return 0;
            }
        }
    }
}
