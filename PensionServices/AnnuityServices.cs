using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pension.Models.ViewModel;
using PensionModel;
using PensionModel.ViewModel;

namespace PensionServices
{
    public class AnnuityServices
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();

        public List<AnnuityViewModel> getannuity()
        {
            try
            {
                Mapper.CreateMap<Annuity1, AnnuityViewModel>();
                List<Annuity1> tblaty = DbContext.Annuity1.Where(m => m.LICId == null).ToList();
                List<AnnuityViewModel> lstmasterdata = Mapper.Map<List<AnnuityViewModel>>(tblaty);
                return lstmasterdata;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("Annuity1", "AnnuityService", "getannuity", ex);
                return null;
            }
        }

        public int Update(int licid, int planid, DateTime effdate, int id)
        {
            try
            {
                Annuity1 objAllo = new Annuity1();
                objAllo = DbContext.Annuity1.SingleOrDefault(m => m.Id == id);
                objAllo.LICId = licid;
                objAllo.PlanID = planid;
                objAllo.EffDate = effdate;
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "PensionMasterService", "Update", ex);
                return 0;
            }
        }

        public List<AnnuityViewModel> lstdata(int id)
        {
            try
            {
                var lstdata = (from val in DbContext.Annuity1
                               join val1 in DbContext.PlanMasters on val.PlanID equals val1.id
                               join val2 in DbContext.InsuranceMasters on val.LICId equals val2.id
                               select new AnnuityViewModel()
                               {
                                   Id = val.Id,
                                   PlanName = val1.PlanName,
                                   Insurance = val2.InsuranceCompany,
                                   LICId = val.LICId,
                                   PlanID = val.PlanID,
                                   Age = val.Age,
                                   Yly = val.Yly,
                                   Hly = val.Hly,
                                   Qly = val.Qly,
                                   Mly = val.Mly,
                                   EffDate = val.EffDate,
                                   Status = val.Status
                               }).ToList();
                return lstdata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int insert(AnnuityViewModel model, int flag)
        {
            try
            {
                if (flag == 1)
                {
                    Mapper.CreateMap<AnnuityViewModel, Annuity1>();
                    Annuity1 objaty = Mapper.Map<Annuity1>(model);
                    DbContext.Annuity1.Add(objaty);
                    return DbContext.SaveChanges();
                }
                else
                {
                    model.Id = model.hdnannuity;
                    Mapper.CreateMap<AnnuityViewModel, Annuity1>();
                    Annuity1 objaty = DbContext.Annuity1.FirstOrDefault(m => m.Id == model.hdnannuity);
                    objaty = Mapper.Map(model, objaty);
                    return DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
