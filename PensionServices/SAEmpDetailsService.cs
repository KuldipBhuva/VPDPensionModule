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
     public class SAEmpDetailsService
     {
         VPDPensionEntities DbContext = new VPDPensionEntities();
         public List<SAEmpDetailsModel> getSABal(int cid, string year)
         {
             try
             {                //Mapper.CreateMap<SAContribution, SAContributionModel>();
                 //List<SAContribution> tblMaster = DbContext.SAContributions.Where(m => m.CompID == cid && m.Year == year && m.Month == Month).ToList();
                 //List<SAContributionModel> lstmasterdata = Mapper.Map<List<SAContributionModel>>(tblMaster);
                 //return lstmasterdata;
 
 
                 var data = (from sa in DbContext.SAEmpDetails
                             join em in DbContext.EmployeeMasters on sa.EmpNo equals em.EmpNo
                             where sa.CompID == cid && sa.Year == year
                             select new SAEmpDetailsModel()
                             {
                                 SAOBID=sa.SAOBID,
                                 SAID = sa.SAID,
                                 EmpNo = sa.EmpNo,
                                 CompID = sa.CompID,
                                 OpeningBalance=sa.OpeningBalance,
                                 Year = sa.Year,
                                 SASettalDate=sa.SASettalDate,
                                 EmpDetails = new EmployeeViewModel()
                                 {
                                     EmpName = em.EmpName
                                 }
                             }).ToList();
                 return data;
 
             }
 
             catch (Exception ex)
             {
                 ErrorLogClass.WriteErrorLog("SABalMaster", "SAEmpDetailsService", "getSABal", ex);
                 return null;
             }
 
 
         }
         public List<SAEmpDetailsModel> getSABal4Update()
         {
             try
             {
                 Mapper.CreateMap<SAEmpDetail, SAEmpDetailsModel>();
                 List<SAEmpDetail> tblMaster = DbContext.SAEmpDetails.Where(m => m.Year == null).ToList();
                 List<SAEmpDetailsModel> lstmasterdata = Mapper.Map<List<SAEmpDetailsModel>>(tblMaster);
                 return lstmasterdata;
 
 
             }
 
             catch (Exception ex)
             {
                 ErrorLogClass.WriteErrorLog("SABalance", "SABalService", "getSABal4Update", ex);
                 return null;
             }
 
 
         }
         public int Update(string year,int cid, int uid, int saobid)
         {
             try
             {
                 SAEmpDetail objAllo = new SAEmpDetail();
                 objAllo = DbContext.SAEmpDetails.SingleOrDefault(m => m.SAOBID == saobid);
                 objAllo.Year = year;                
                 objAllo.CompID = cid;
                 objAllo.UploadedBy = uid;
                 objAllo.UploadedDate = System.DateTime.Now;
                 return DbContext.SaveChanges();
 
             }
 
             catch (Exception ex)
             {
                 ErrorLogClass.WriteErrorLog("AdminManagement", "SABalService", "Update", ex);
                 return 0;
             }
         }
     }
 }