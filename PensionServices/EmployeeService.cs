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
    public class EmployeeService
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        public List<EmployeeViewModel> getRetireEmp(int cid)
        {
            try
            {
                var data = (from em in DbContext.EmployeeMasters
                            join cm in DbContext.CompanyMasters on em.CompId equals cm.id
                            //join gm in DbContext.GradeMasters on em.GradeId equals gm.id
                            //join ep in DbContext.EmployeePlans on em.EmpId equals ep.EmpId into plans
                            //from e in plans.DefaultIfEmpty().Where(m=>m.RPensionType==1)
                            //join pm in DbContext.PensionMasters on e.AnnuityNo equals pm.AnnuityNo into pension
                            //from p in pension.DefaultIfEmpty()
                            where em.CompId == cid && em.RetireDate!=null
                            select new EmployeeViewModel()
                            {
                                CompDetails = new CompanyModel()
                                {
                                    CompName = cm.CompName,
                                    CompCode = cm.CompCode
                                },
                                //AnnuityDetails=new EmployeePlanViewModel()
                                //{
                                //    AnnuityNo = (e == null ? string.Empty : e.AnnuityNo),
                                //},
                                //PensionDetails=new PensionMasterModel()
                                //{
                                //    PolicyNo = (p == null ? string.Empty : p.PolicyNo),
                                //},
                                EmpId = em.EmpId,
                                CompId = em.CompId,
                                EmpNo = em.EmpNo,
                                EmpName = em.EmpName,
                                Email = em.Email,
                                EmployeeType = em.EmployeeType,
                                HoldDate = em.HoldDate,
                                Remarks = em.Remarks,
                                Status = em.Status,
                                GradeId = em.GradeId,
                                Benefits = em.Benefits,
                                Contribution = em.Contribution,
                                EmployeementType = em.EmployeementType,
                                AnnuityMode = em.AnnuityMode,
                                DOJ = em.DOJ,
                                RetireDate = em.RetireDate,
                                LICSubmit=em.LICSubmit,
                                LICSubmitDate=em.LICSubmitDate
                            }).ToList();
                return data;
            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("Dashboard", "EmployeeService", "getRetireEmp", ex);
                return null;
            }
        }
        public List<EmployeeViewModel> getAllEmp(int cid)
        {
            try
            {
                var data = (from em in DbContext.EmployeeMasters
                            join cm in DbContext.CompanyMasters on em.CompId equals cm.id
                            //join gm in DbContext.GradeMasters on em.GradeId equals gm.id
                            where em.CompId == cid
                            select new EmployeeViewModel()
                            {
                                CompDetails = new CompanyModel()
                                {
                                    CompName = cm.CompName,
                                    CompCode = cm.CompCode
                                },
                                EmpId = em.EmpId,
                                CompId = em.CompId,
                                EmpNo = em.EmpNo,
                                EmpName = em.EmpName,
                                Email = em.Email,
                                EmployeeType = em.EmployeeType,
                                HoldDate = em.HoldDate,
                                Remarks = em.Remarks,
                                Status = em.Status,
                                GradeId = em.GradeId,
                                Benefits = em.Benefits,
                                Contribution = em.Contribution,
                                EmployeementType = em.EmployeementType,
                                AnnuityMode = em.AnnuityMode,
                                DOJ = em.DOJ,
                                RetireDate = em.RetireDate
                            }).ToList();
                return data;
            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("CompanyMaster", "CompanyService", "lstCompany", ex);
                return null;
            }
        }

        public int insertEmp(EmployeeViewModel model)
        {
            Mapper.CreateMap<EmployeeViewModel, EmployeeMaster>();
            EmployeeMaster objCompany = Mapper.Map<EmployeeMaster>(model);
            DbContext.EmployeeMasters.Add(objCompany);

            GradeChange objgrade = new GradeChange();
            objgrade.Compnay_id = model.CompId;
            objgrade.EmpNo = objCompany.EmpId;
            objgrade.GradeChangeTo = Convert.ToInt32(model.GradeId);
            DbContext.GradeChanges.Add(objgrade);

            return DbContext.SaveChanges();
        }

        public EmployeeViewModel GetbyID(int id)
        {
            try
            {
                var data = (from em in DbContext.EmployeeMasters
                            join cm in DbContext.CompanyMasters on em.CompId equals cm.id
                            join gm in DbContext.GradeMasters on em.GradeId equals gm.id
                            where em.EmpId == id
                            select new EmployeeViewModel()
                            {
                                CompDetails = new CompanyModel()
                                {
                                    CompName = cm.CompName,
                                    CompCode = cm.CompCode
                                },
                                EmpId = em.EmpId,
                                CompId = em.CompId,
                                EmpNo = em.EmpNo,
                                EmpName = em.EmpName,
                                DOB = em.DOB,
                                DOJ = em.DOJ,
                                RetireTypeId = em.RetireTypeId,
                                RetireDate = em.RetireDate,
                                DeathDate = em.DeathDate,
                                Gender = em.Gender,
                                MaritalStatus = em.MaritalStatus,
                                FHStatus = em.FHStatus,
                                FHName = em.FHName,
                                Relation = em.Relation,
                                Location = em.Location,
                                Address = em.Address,
                                ContactO = em.ContactO,
                                ContactR = em.ContactR,
                                Email = em.Email,
                                AadharNo = em.AadharNo,
                                PANNo = em.PANNo,
                                BankAcNo = em.BankAcNo,
                                BankName = em.BankName,
                                AccType = em.AccType,
                                IFSC = em.IFSC,
                                BranchName = em.BankName,
                                BranchAddress = em.BranchAddress,
                                PensionStatus = em.PensionStatus,
                                Status = em.Status,
                                PentionTypeID = em.PentionTypeID,
                                EmployeementType = em.EmployeementType,
                                GradeId = em.GradeId,
                                EmployeeType = em.EmployeeType,
                                HoldDate = em.HoldDate,
                                Remarks = em.Remarks,
                                AnnuityMode = em.AnnuityMode,
                                Merged = em.Merged,
                                FromMerger = em.FromMerger,
                                MDOJ = em.MDOJ,
                                MDOL = em.MDOL,
                                FrozenAmt = em.FrozenAmt,
                                FrozenPensionAmt = em.FrozenPensionAmt,
                                Benefits = em.Benefits,
                                Contribution = em.Contribution,
                                RPensionAmt = em.RPensionAmt,
                                SASettleDate = em.SASettleDate,
                                GradeName = gm.Grade_Name,
                                LicId = em.LicId,
                                PaymentType = em.PaymentType
                            }).SingleOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("CompanyMaster", "CompanyService", "lstCompany", ex);
                return null;
            }
            //    Mapper.CreateMap<EmployeeMaster, EmployeeViewModel>();
            //    EmployeeMaster objCompanyMaster = DbContext.EmployeeMasters.Where(m => m.EmpId == id).SingleOrDefault();
            //    EmployeeViewModel objCompItem = Mapper.Map<EmployeeViewModel>(objCompanyMaster);
            //return objCompItem;           
        }

        public int Update(EmployeeViewModel model)
        {
            Mapper.CreateMap<EmployeeViewModel, EmployeeMaster>();
            EmployeeMaster objComp = DbContext.EmployeeMasters.SingleOrDefault(m => m.EmpId == model.EmpId);
            objComp = Mapper.Map(model, objComp);
            return DbContext.SaveChanges();
        }

        public EmployeeViewModel CheckExists(string eno, int cc)
        {
            try
            {
                var EmpData = (from emp in DbContext.EmployeeMasters
                                where emp.CompId == cc && emp.EmpNo == eno
                                select new EmployeeViewModel()
                                {
                                    EmpNo = emp.EmpNo
                                }).SingleOrDefault();

                return EmpData;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "EmployeeService", "CheckExists", ex);
                return null;
            }
        }

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
                ErrorLogClass.WriteErrorLog("AdminManagement", "EmployeeController", "lstRetirement", ex);
                return null;
            }
        }
    }
}
