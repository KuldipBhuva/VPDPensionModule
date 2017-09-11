using PensionModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PensionServices;
using Pension.Models;
using PensionModel;
using Pension.Models.ViewModel;

namespace PensionServices
{
   public class EmpPensionServices
    {
       VPDPensionEntities DbContext = new VPDPensionEntities();
       public List<EmployeePensionVM> Employeewisepension(int emp, string year)
       {
           try
           {
               var lstdata = (from PM in DbContext.PensionMasters
                              join EP in DbContext.EmployeePlans on PM.AnnuityNo equals EP.AnnuityNo
                              join EM in DbContext.EmployeeMasters on EP.EmpId equals EM.EmpId
                              where EM.EmpId == emp && PM.Year == year
                              select new EmployeePensionVM()
                              {
                                  AnnuityNo = EP.AnnuityNo,
                                  CheqAmt = PM.CheqAmt,
                                  PolicyNo = EP.PolicyId,
                                  GrossAmt = PM.GrossAmt,
                                  Month = PM.PensionMonth,
                                  IsProcessed=PM.IsProcessed,
                                  ITAmt=PM.ITAmt,

                              }).ToList();
               return lstdata;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public List<EmployeePensionVM> Allpension()
       {
           try
           {
               var lstdata = (from PM in DbContext.PensionMasters
                              join EP in DbContext.EmployeePlans on PM.AnnuityNo equals EP.AnnuityNo
                              join EM in DbContext.EmployeeMasters on EP.EmpId equals EM.EmpId                             
                              select new EmployeePensionVM()
                              {
                                  AnnuityNo = EP.AnnuityNo,
                                  CheqAmt = PM.CheqAmt,
                                  PolicyNo = EP.PolicyId,
                                  GrossAmt = PM.GrossAmt,
                                  Month = PM.PensionMonth

                              }).ToList();
               return lstdata;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
