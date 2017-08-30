using Pension.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Pension.Services;
using Pension.Models;
using PensionModel;
namespace Pension.Services
{
    public interface IAllowanceServices
    {
        List<AllowanceViewModel> lstAllow();
        int InsertAllow(AllowanceViewModel model);
        AllowanceViewModel GetById(int AlloId);
        int UpdateAllowanceData(int Id, AllowanceViewModel model);
        AllowanceViewModel CheckExists(string Allow);
    }

    public class AllowanceServices : IAllowanceServices, IDisposable
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        public List<AllowanceViewModel> lstAllow()
        {
            try
            {
                var lstAllow = (from Allo in DbContext.AllowanceMasters
                                select new AllowanceViewModel()
                                {
                                    id = Allo.id,
                                    AllowanceName = Allo.AllowanceName,
                                    Status = Allo.Status,
                                    Description = Allo.Description

                                }).ToList();

                return lstAllow;
            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "AllowanceController", "lstAllow", ex);
                return null;
            }


        }

        public int InsertAllow(AllowanceViewModel model)
        {
            try
            {
                AllowanceMaster objAllo = new AllowanceMaster();
                objAllo.AllowanceName = model.AllowanceName;
                objAllo.Description = model.Description;
                objAllo.Status = model.Status;

                DbContext.AllowanceMasters.Add(objAllo);
                DbContext.SaveChanges();
                return objAllo.id;

            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "AllowanceController", "InsertAllow", ex);
                return 0;
            }
        }

        public AllowanceViewModel GetById(int AlloId)
        {
            try
            {
                var lstcomp = (from Allo in DbContext.AllowanceMasters
                               where Allo.id == AlloId
                               select new AllowanceViewModel()
                               {
                                   id = Allo.id,
                                   AllowanceName = Allo.AllowanceName,
                                   Description = Allo.Description,
                                   Status = Allo.Status

                               }).FirstOrDefault();

                return lstcomp;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "AllowanceController", "GetById", ex);
                return null;
            }
        }

        public int UpdateAllowanceData(int Id, AllowanceViewModel model)
        {
            try
            {
                AllowanceMaster objAllo = new AllowanceMaster();
                objAllo = DbContext.AllowanceMasters.SingleOrDefault(m => m.id == Id);
                objAllo.AllowanceName = model.AllowanceName;
                objAllo.Description = model.Description;
                objAllo.Status = model.Status;
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "AllowanceController", "UpdateAllowanceData", ex);
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
                ErrorLogClass.WriteErrorLog("AdminManagement", "AllowanceController", "CheckExists", ex);
                return null;
            }
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}