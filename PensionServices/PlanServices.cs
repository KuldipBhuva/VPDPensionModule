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
    public interface IPlanServices
    {
        List<PlanViewModel> lstPlan();
        int InsertPlan(PlanViewModel model);
        int UpdateIns(int Id, PlanViewModel model);
        PlanViewModel GetById(int id);
    }

    public class PlanServices : IPlanServices
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        public List<PlanViewModel> lstPlan()
        {
            try
            {
                var lstEmployeer = (from plan in DbContext.PlanMasters
                                    select new PlanViewModel()
                                    {
                                        id = plan.id,
                                        CompID = plan.CompID,
                                        Details = plan.Details,
                                        Formula = plan.Formula,
                                        PlanName = plan.PlanName,
                                        EffDate = plan.EffDate,
                                        Status = plan.Status,

                                    }).ToList();

                return lstEmployeer;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "PlanController", "lstPlan", ex);
                return null;
            }
        }

        public List<PlanViewModel> lstPlanins(int id)
        {
            try
            {
                var lstEmployeer = (from plan in DbContext.PlanMasters
                                    where plan.CompID == id
                                    select new PlanViewModel()
                                    {
                                        id = plan.id,
                                        CompID = plan.CompID,
                                        Details = plan.Details,
                                        Formula = plan.Formula,
                                        PlanName = plan.PlanName,
                                        EffDate = plan.EffDate,
                                        Status = plan.Status,

                                    }).ToList();

                return lstEmployeer;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "PlanController", "lstPlan", ex);
                return null;
            }
        }

        public int InsertPlan(PlanViewModel model)
        {
            try
            {
                PlanMaster objPlan = new PlanMaster();
                objPlan.CompID = model.CompID;
                objPlan.Details = model.Details;
                objPlan.EffDate = model.EffDate;
                objPlan.Formula = model.Formula;
                objPlan.PlanName = model.PlanName;
                objPlan.Status = model.Status;

                DbContext.PlanMasters.Add(objPlan);
                DbContext.SaveChanges();
                return objPlan.id;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "PlanController", "InsertPlan", ex);
                return 0;
            }
        }

        public PlanViewModel GetById(int id)
        {
            try
            {
                var lstGrd = (from plan in DbContext.PlanMasters
                              where plan.id == id
                              select new PlanViewModel()
                              {
                                  id = plan.id,
                                  CompID = plan.CompID,
                                  Details = plan.Details,
                                  Formula = plan.Formula,
                                  PlanName = plan.PlanName,
                                  EffDate = plan.EffDate,
                                  Status = plan.Status,

                              }).FirstOrDefault();

                return lstGrd;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "PlanController", "GetById", ex);
                return null;
            }


        }

        public int UpdateIns(int Id, PlanViewModel model)
        {
            try
            {
                PlanMaster objPlan = new PlanMaster();
                objPlan = DbContext.PlanMasters.SingleOrDefault(m => m.id == Id);
                objPlan.CompID = model.CompID;
                objPlan.Details = model.Details;
                objPlan.EffDate = model.EffDate;
                objPlan.Formula = model.Formula;
                objPlan.PlanName = model.PlanName;
                objPlan.Status = model.Status;

                return DbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "PlanController", "UpdateIns", ex);
                return 0;
            }

        }

    }
}