using PensionModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PensionServices;
using Pension.Models;
using PensionModel;
using Pension.Models.ViewModel;
namespace Pension.Services
{
    public class CompWiseAlloService
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();

        public List<AllowanceViewModel> lstAllow(int CompID, int flag)
        {
            try
            {
                if (flag == 0)
                {
                    var lstAllow = (from Allo in DbContext.AllowanceMasters
                                    //where !(from b in DbContext.CompWiseAllowances
                                    //        where b.AllowanceID == b.AllowanceID && b.GradeID==b.GradeID && b.CompID == CompID
                                    //        select b.AllowanceID)
                                    //          .Contains(Allo.id)
                                    select new AllowanceViewModel()
                                    {
                                        id = Allo.id,
                                        AllowanceName = Allo.AllowanceName,
                                        Status = Allo.Status,
                                        Description = Allo.Description
                                    }).ToList();
                    return lstAllow;
                }
                else
                {
                    var lstAllow = (from Allo in DbContext.AllowanceMasters
                                    where !(from b in DbContext.CompWiseAllowances
                                            where b.AllowanceID == b.AllowanceID && b.GradeID == b.GradeID && b.CompID == CompID
                                            select b.AllowanceID)
                                             .Contains(Allo.id)
                                    select new AllowanceViewModel()
                                    {
                                        id = Allo.id,
                                        AllowanceName = Allo.AllowanceName,
                                        Status = Allo.Status,
                                        Description = Allo.Description
                                    }).ToList();
                    return lstAllow;
                }
            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "AllowanceController", "lstAllow", ex);
                return null;
            }


        }

        public List<CompWiseAllowViewModel> lstCompAllow(int id)
        {
            try
            {
                var lstAllow = (from Allo in DbContext.CompWiseAllowances
                                join Comp in DbContext.CompanyMasters on Allo.CompID equals Comp.id
                                join Allow in DbContext.AllowanceMasters on Allo.AllowanceID equals Allow.id
                                join grd in DbContext.GradeMasters on Allo.GradeID equals grd.id
                                where Comp.id == id //&& Allo.IsChecked == true
                                select new CompWiseAllowViewModel()
                                {
                                    id = Allo.id,
                                    CompID = Allo.CompID,
                                    AllowanceID = Allo.AllowanceID,
                                    CompName = Comp.CompName,
                                    AllowName = Allow.AllowanceName,
                                    EffDeate = Allo.EffDeate,
                                    GradeID = Allo.GradeID,
                                    Grade = grd.Grade_Name,
                                    Status = Allo.Status
                                }).ToList();

                return lstAllow;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyWiseAllowanceController", "lstCompAllow", ex);
                return null;
            }
        }

        public int InsertAllow(CompWiseAllowViewModel model)
        {
            try
            {
                CompWiseAllowance objAllo = new CompWiseAllowance();
                objAllo.CompID = model.CompID;
                objAllo.GradeID = model.GradeID;
                objAllo.AllowanceID = model.AllowanceID;
                objAllo.EffDeate = model.EffDeate;
                objAllo.IsChecked = model.IsChecked;
                objAllo.Status = model.Status;

                DbContext.CompWiseAllowances.Add(objAllo);
                DbContext.SaveChanges();
                return objAllo.id;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyWiseAllowanceController", "InsertAllow", ex);
                return 0;
            }
        }

        public CompWiseAllowViewModel GetById(int AlloId)
        {
            try
            {
                var lstcomp = (from Allo in DbContext.CompWiseAllowances
                               where Allo.id == AlloId
                               select new CompWiseAllowViewModel()
                               {
                                   id = Allo.id,
                                   CompID = Allo.CompID,
                                   AllowanceID = Allo.AllowanceID,
                                   EffDeate = Allo.EffDeate,
                                   GradeID = Allo.GradeID,
                                   Status = Allo.Status
                               }).FirstOrDefault();

                return lstcomp;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyWiseAllowanceController", "GetById", ex);
                return null;
            }
        }

        public int UpdateAllowanceData(int Id, CompWiseAllowViewModel model)
        {
            try
            {
                CompWiseAllowance objAllo = new CompWiseAllowance();
                objAllo = DbContext.CompWiseAllowances.SingleOrDefault(m => m.id == Id);
                objAllo.CompID = model.CompID;
                objAllo.GradeID = model.GradeID;
                objAllo.AllowanceID = model.AllowanceID;
                objAllo.EffDeate = model.EffDeate;
                objAllo.IsChecked = model.IsChecked;
                objAllo.Status = model.Status;

                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyWiseAllowanceController", "UpdateAllowanceData", ex);
                return 0;
            }
        }

        public AllowanceViewModel CheckExists(string Allow)
        {
            try
            {
                var CompData = (from Allo in DbContext.AllowanceMasters
                                where Allo.AllowanceName == Allow
                                select new AllowanceViewModel()
                                {
                                    AllowanceName = Allo.AllowanceName
                                }).SingleOrDefault();

                return CompData;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyWiseAllowanceController", "CheckExists", ex);
                return null;
            }
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}