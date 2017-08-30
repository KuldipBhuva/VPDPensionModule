using AutoMapper;
using Pension.Models.ViewModel;
using PensionModel;
using PensionModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pension.Services
{
    public interface IRetirementTypeService
    {
        List<RetirementTypeViewModel> lstRetirement();
        int InsertPlan(RetirementTypeViewModel model);
        RetirementTypeViewModel GetById(int id);
        int UpdateIns(int Id, RetirementTypeViewModel model);
        RetirementTypeViewModel CheckExists(string RetirementType);

    }

    public class RetirementTypeService : IRetirementTypeService
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        public List<RetirementTypeViewModel> lstRetirement()
        {
            try
            {
                var lstEmployeer = (from Retire in DbContext.RetirementType_Master
                                    select new RetirementTypeViewModel()
                                    {
                                        id = Retire.id,
                                        RetirementType = Retire.RetirementType,
                                        Description = Retire.Description,
                                        Status = Retire.Status,

                                    }).ToList();

                return lstEmployeer;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "RetirementController", "lstRetirement", ex);
                return null;
            }


        }

        public int InsertPlan(RetirementTypeViewModel model)
        {
            try
            {
                RetirementType_Master objRetire = new RetirementType_Master();
                objRetire.RetirementType = model.RetirementType;
                objRetire.Description = model.Description;
                objRetire.Status = model.Status;


                DbContext.RetirementType_Master.Add(objRetire);
                DbContext.SaveChanges();
                return objRetire.id;

            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "RetirementController", "InsertPlan", ex);
                return 0;
            }


        }

        public RetirementTypeViewModel GetById(int id)
        {
            try
            {
                var lstGrd = (from Retire in DbContext.RetirementType_Master
                              where Retire.id == id
                              select new RetirementTypeViewModel()
                              {
                                  id = Retire.id,
                                  RetirementType = Retire.RetirementType,
                                  Description = Retire.Description,
                                  Status = Retire.Status,

                              }).FirstOrDefault();

                return lstGrd;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "RetirementController", "GetById", ex);
                return null;
            }



        }

        public int UpdateIns(int Id, RetirementTypeViewModel model)
        {
            try
            {
                RetirementType_Master objRetire = new RetirementType_Master();
                objRetire = DbContext.RetirementType_Master.SingleOrDefault(m => m.id == Id);
                objRetire.RetirementType = model.RetirementType;
                objRetire.Description = model.Description;
                objRetire.Status = model.Status;
                return DbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "RetirementController", "UpdateIns", ex);
                return 0;
            }
        }
        public RetirementTypeViewModel CheckExists(string RetirementType)
        {
            try
            {
                var CompData = (from Allo in DbContext.RetirementType_Master
                                where Allo.RetirementType == RetirementType
                                select new RetirementTypeViewModel()
                                {
                                    RetirementType = Allo.RetirementType
                                }).SingleOrDefault();

                return CompData;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "RetirementController", "CheckExists", ex);
                return null;
            }
        }



    }
}