using AutoMapper;
using Pension.Models.ViewModel;
using PensionModel;
using PensionModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionServices
{
    public class SAService
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        public List<SAContributionModel> getSA(int cid, string year, string Month)
        {
            try
            {                //Mapper.CreateMap<SAContribution, SAContributionModel>();
                //List<SAContribution> tblMaster = DbContext.SAContributions.Where(m => m.CompID == cid && m.Year == year && m.Month == Month).ToList();
                //List<SAContributionModel> lstmasterdata = Mapper.Map<List<SAContributionModel>>(tblMaster);
                //return lstmasterdata;


                var data = (from sa in DbContext.SAContributions
                            join em in DbContext.EmployeeMasters on sa.EmpNo equals em.EmpNo
                            where sa.CompID == cid && sa.Year == year && sa.Month == Month
                            select new SAContributionModel()
                            {
                                SAID = sa.SAID,
                                EmpNo = sa.EmpNo,
                                CompID = sa.CompID,
                                Basic = sa.Basic,
                                CompCont = sa.CompCont,
                                EmpCont = sa.EmpCont,
                                Month = sa.Month,
                                Year = sa.Year,
                                EmpDetails = new EmployeeViewModel()
                                {
                                    EmpName=em.EmpName
                                }
                            }).ToList();
                return data;

            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("SAContMaster", "SAService", "getSA", ex);
                return null;
            }


        }
        public List<SAContributionModel> getSA4Update()
        {
            try
            {
                Mapper.CreateMap<SAContribution, SAContributionModel>();
                List<SAContribution> tblMaster = DbContext.SAContributions.Where(m => m.Year == null).ToList();
                List<SAContributionModel> lstmasterdata = Mapper.Map<List<SAContributionModel>>(tblMaster);
                return lstmasterdata;


            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("SAMaster", "SAMasterService", "getSA4Update", ex);
                return null;
            }


        }
        public int Update(string year, string month, int cid, int uid, int said)
        {
            try
            {
                SAContribution objAllo = new SAContribution();
                objAllo = DbContext.SAContributions.SingleOrDefault(m => m.SAID == said);
                objAllo.Year = year;
                objAllo.Month = month;
                objAllo.CompID = cid;
                objAllo.UploadedBy = uid;
                objAllo.UploadedDate = System.DateTime.Now;
                return DbContext.SaveChanges();

            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "SAService", "Update", ex);
                return 0;
            }
        }
        //public List<SAContributionModel> getSAByTime(string year, string month, int cid)
        //{
        //    try
        //    {
        //        Mapper.CreateMap<SAContribution, SAContributionModel>();
        //        List<SAContribution> tblMaster = DbContext.SAContributions.Where(m => m.CompID == cid && m.Year == year && m.Month == month).ToList();
        //        List<SAContributionModel> lstmasterdata = Mapper.Map<List<SAContributionModel>>(tblMaster);
        //        return lstmasterdata;


        //    }

        //    catch (Exception ex)
        //    {
        //        ErrorLogClass.WriteErrorLog("SalaryMaster", "SalaryMasterService", "getSalary", ex);
        //        return null;
        //    }


        //}
    }
}
