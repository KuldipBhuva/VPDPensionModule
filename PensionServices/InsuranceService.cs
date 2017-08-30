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
    public interface IInsuranceService
    {
        List<InsuranceViewModel> lstIns();
        int InsertIns(InsuranceViewModel model);
        InsuranceViewModel GetById(int id);
        int UpdateIns(int Id, InsuranceViewModel model);
       
    }


    public class InsuranceService : IInsuranceService,IDisposable
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        public List<InsuranceViewModel> lstIns()
        {
            try
            {
                var lstEmployeer = (from grd in DbContext.InsuranceMasters
                                    select new InsuranceViewModel()
                                    {
                                        id = grd.id,
                                        InsuranceCompany = grd.InsuranceCompany,
                                        HO = grd.HO,
                                        Status = grd.Status,

                                    }).ToList();

                return lstEmployeer;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "InsuranceController", "lstIns", ex);
                return null;
            }


        }

        public int InsertIns(InsuranceViewModel model)
        {
            try
            {
                InsuranceMaster objIns = new InsuranceMaster();
                objIns.InsuranceCompany = model.InsuranceCompany;
                objIns.HO = model.HO;
                objIns.Status = model.Status;

                DbContext.InsuranceMasters.Add(objIns);
                DbContext.SaveChanges();
                return objIns.id;

            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "InsuranceController", "InsertIns", ex);
                return 0;
            }


        }

        public InsuranceViewModel GetById(int id)
        {
            try
            {
                var lstGrd = (from ins in DbContext.InsuranceMasters
                              where ins.id == id
                              select new InsuranceViewModel()
                              {
                                  id = ins.id,
                                  InsuranceCompany = ins.InsuranceCompany,
                                  HO = ins.HO,
                                  Status = ins.Status

                              }).FirstOrDefault();

                return lstGrd;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "InsuranceController", "GetById", ex);
                return null;
            }


        }

        public int UpdateIns(int Id, InsuranceViewModel model)
        {
            try
            {
                InsuranceMaster objIns = new InsuranceMaster();
                objIns = DbContext.InsuranceMasters.SingleOrDefault(m => m.id == Id);
                objIns.InsuranceCompany = model.InsuranceCompany;
                objIns.HO = model.HO;
                objIns.Status = model.Status;

                return DbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "InsuranceController", "UpdateIns", ex);
                return 0;
            }

        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}