using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PensionModel.ViewModel;
using PensionServices;
using PensionModel;
using Pension.Models.ViewModel;
using AutoMapper;

namespace PensionServices
{
    public class EmployeePlanServices
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();

        public List<EmployeePlanViewModel> lstdata(int id)
        {
            try
            {
                var lstdata = (from val in DbContext.EmployeePlans
                               join em in DbContext.EmployeeMasters on val.EmpId equals em.EmpId
                               join val1 in DbContext.PlanMasters on em.PentionTypeID equals val1.id
                               join val2 in DbContext.InsuranceMasters on em.LicId equals val2.id
                               where val.EmpId == id
                               select new EmployeePlanViewModel()
                               {
                                   Id = val.Id,
                                   EmpId = val.EmpId,
                                   Plan = val1.PlanName,
                                   Insurance = val2.InsuranceCompany,
                                   PensionId = val.PensionId,
                                   LicId = val.LicId,
                                   PlanId = val.PlanId,
                                   AnnuityNo = val.AnnuityNo,
                                   InsuranceOffife = val.InsuranceOffife,
                                   Email = val.Email,
                                   PolicyId = val.PolicyId,
                                   PolicyDate = val.PolicyDate,
                                   Amount = val.Amount,
                                   PolicyStatus = val.PolicyStatus,
                                   HoldDate = val.HoldDate,
                                   Remark = val.Remark
                               }).ToList();
                return lstdata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmployeePlanViewModel Data(int id)
        {
            var lst = (from val in DbContext.EmployeeMasters
                       join val1 in DbContext.PlanMasters on val.PentionTypeID equals val1.id
                       join val2 in DbContext.InsuranceMasters on val.LicId equals val2.id
                       where val.EmpId == id
                       select new PensionModel.ViewModel.EmployeePlanViewModel()
                       {
                           Plan = val1.PlanName,
                           Insurance = val2.InsuranceCompany
                       }).SingleOrDefault();
            return lst;
        }

        public int insert(EmployeePlanViewModel model, int flag)
        {
            try
            {
                EmployeeMaster emp = new EmployeeMaster();
                emp = DbContext.EmployeeMasters.SingleOrDefault(m => m.EmpId == model.EmpId);
                model.LicId = emp.LicId;
                model.PlanId = emp.PentionTypeID;
                if (flag == 1)
                {
                    Mapper.CreateMap<EmployeePlanViewModel, EmployeePlan>();
                    EmployeePlan objempplan = Mapper.Map<EmployeePlan>(model);
                    DbContext.EmployeePlans.Add(objempplan);
                    return DbContext.SaveChanges();
                }
                else
                {
                    model.Id = model.hdnempplan;
                    Mapper.CreateMap<EmployeePlanViewModel, EmployeePlan>();
                    EmployeePlan objempplan = DbContext.EmployeePlans.FirstOrDefault(m => m.Id == model.hdnempplan);
                    objempplan = Mapper.Map(model, objempplan);
                    return DbContext.SaveChanges();
                    //return Convert.ToInt32(objempplan.Id);


                    //EmployeePlan empmst = new EmployeePlan();
                    //empmst = DbContext.EmployeePlans.FirstOrDefault(m => m.Id == model.hdnempplan);
                    //empmst.LicId = model.LicId;
                    //empmst.PlanId = model.PlanId;
                    //empmst.AnnuityNo = model.AnnuityNo;
                    //empmst.InsuranceOffife = model.InsuranceOffife;
                    //empmst.Email = model.Email;
                    //empmst.PolicyId = model.PolicyId;
                    //empmst.PolicyDate = model.PolicyDate;
                    //empmst.Amount = model.Amount;
                    //return DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmployeePlanViewModel GetByID(int id)
        {
            Mapper.CreateMap<EmployeePlan, EmployeePlanViewModel>();
            EmployeePlan objempplan = DbContext.EmployeePlans.Where(m => m.Id == id).SingleOrDefault();
            EmployeePlanViewModel objeplan = Mapper.Map<EmployeePlanViewModel>(objempplan);
            return objeplan;
        }
    }
}
