using AutoMapper;
using Pension.Models.ViewModel;
using PensionModel;
using PensionModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionServices
{
    public class ColunmMappingService
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        public List<SalaryModel> getSalaryTable(int stype)
        {
            try
            {

                Mapper.CreateMap<SalaryMaster, SalaryModel>();
                List<SalaryMaster> tblMaster = DbContext.SalaryMasters.ToList();
                List<SalaryModel> lstmasterdata = Mapper.Map<List<SalaryModel>>(tblMaster);
                return lstmasterdata;


                //var data = (from pm in DbContext.PensionMasters
                //            join em in DbContext.EmployeeMasters on pm.EmpNo equals em.EmpNo
                //            where em.CompId == cid &&(pm.PensionMonth==null || pm.Year==null)
                //            select new PensionMasterModel()
                //            {                              
                //                PID=pm.PID,
                //                PensionMonth=pm.PensionMonth,
                //                Year=pm.Year

                //            }).ToList();
                //return data;
            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("ColunmMapping", "ColunmMappingService", "getTableColunm", ex);
                return null;
            }


        }
        public List<ColunmMapModel> getColumn(string table)
        {
            //var tbl = new SqlParameter("@table", table);
            //List<TableColunm> empList = DbContext.Database.SqlQuery<TableColunm>("exec sp_column @table", tbl).ToList<TableColunm>();
            //return empList;
            var tripList = DbContext.Database.SqlQuery<ColunmMapModel>
                ("EXEC sp_column @table", new SqlParameter("table", table)).ToList<ColunmMapModel>();
            //DbContext.Database.SqlQuery<PayrollItem>("exec PayrollReport", params1).ToList<PayrollItem>();
            return tripList;
        }
        public int Insert(int compid,string tbl,string tblClm,int tblIndex,string exlClm,int mapBy,DateTime mapDate)
        {
           try
            {
                ColunmMapMaster obj = new ColunmMapMaster();
                obj.CompID = compid;
                obj.TblName = tbl;
                obj.TblClmName = tblClm;
                obj.TblClmIndex = tblIndex;
                obj.ExlClmName = exlClm;
                obj.MappedBy = mapBy;
                obj.MappedDate = mapDate;
                DbContext.ColunmMapMasters.Add(obj);
                return DbContext.SaveChanges();                
            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("ColumnMapingMaster", "ColumnMappingService", "InsertColMap", ex);
                return 0;
            }
        }
    }
}
