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
    public interface IEmployeeService
    {
        int InsertEmpoyeeData(EmployeeViewModel model, int flag);
        List<EmployeeViewModel> getAllEmp(int cid);
        List<RetirementTypeViewModel> lstRetirement();
        List<CompanyModel> lstCompany();
        EmployeeViewModel getEmpbyID(int id);
    }


    public class EmployeeService : IEmployeeService, IDisposable
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();

        public int InsertEmpoyeeData(EmployeeViewModel model, int flag)
        {
            try
            {
                if (flag == 1)
                {
                    EmployeeMaster objemp = new EmployeeMaster();
                    objemp.EmpNo = model.EmpNo;
                    objemp.EmpName = model.EmpName;
                    objemp.DOJ = model.DOJ;
                    objemp.DOB = model.DOB;
                    objemp.Address = model.Address;
                    objemp.Email = model.Email;
                    objemp.IFSC = model.IFSC;
                    objemp.Location = model.Location;
                    objemp.MaritalStatus = model.MaritalStatus;
                    objemp.PANNo = model.PANNo;
                    objemp.AadharNo = model.AadharNo;
                    objemp.AccType = model.AccType;
                    objemp.BankAcNo = model.BankAcNo;
                    objemp.BankName = model.BankName;
                    objemp.CompId = model.CompId;
                    objemp.RetireTypeId = model.RetireTypeId;
                    objemp.RetireDate = model.RetireDate;
                    objemp.DeathDate = model.DeathDate;
                    objemp.Gender = model.Gender;
                    objemp.FHStatus = model.FHStatus;
                    objemp.Relation = model.Relation;
                    objemp.ContactO = model.ContactO;
                    objemp.ContactR = model.ContactR;
                    objemp.BranchName = model.BranchName;
                    objemp.BranchAddress = model.BranchAddress;
                    objemp.PensionStatus = model.PensionStatus;
                    objemp.Status = model.Status;
                    objemp.CreatedBy = model.CreatedBy;
                    objemp.CreatedDate = System.DateTime.Now;
                    DbContext.EmployeeMasters.Add(objemp);
                    DbContext.SaveChanges();
                    return objemp.EmpId;
                }
                else
                {
                    EmployeeMaster objemp = new EmployeeMaster();
                    objemp = DbContext.EmployeeMasters.SingleOrDefault(m => m.EmpId == model.hdnEmpId);
                    objemp.EmpName = model.EmpName;
                    objemp.DOJ = model.DOJ;
                    objemp.DOB = model.DOB;
                    objemp.Address = model.Address;
                    objemp.Email = model.Email;
                    objemp.IFSC = model.IFSC;
                    objemp.Location = model.Location;
                    objemp.MaritalStatus = model.MaritalStatus;
                    objemp.PANNo = model.PANNo;
                    objemp.AadharNo = model.AadharNo;
                    objemp.AccType = model.AccType;
                    objemp.BankAcNo = model.BankAcNo;
                    objemp.BankName = model.BankName;
                    objemp.CompId = model.CompId;
                    objemp.RetireTypeId = model.RetireTypeId;
                    objemp.RetireDate = model.RetireDate;
                    objemp.DeathDate = model.DeathDate;
                    objemp.Gender = model.Gender;
                    objemp.FHStatus = model.FHStatus;
                    objemp.Relation = model.Relation;
                    objemp.ContactO = model.ContactO;
                    objemp.ContactR = model.ContactR;
                    objemp.BranchName = model.BranchName;
                    objemp.BranchAddress = model.BranchAddress;
                    objemp.PensionStatus = model.PensionStatus;
                    objemp.Status = model.Status;
                    objemp.UpdatedBy = model.UpdatedBy;
                    objemp.CreatedDate = System.DateTime.Now;
                    objemp.Status = model.Status;

                    return DbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "EmployeeService", "InsertEmpoyeeData", ex);
                return 0;
            }

        }

        public List<EmployeeViewModel> getAllEmp(int cid)
        {
            try
            {
                var data = (from em in DbContext.EmployeeMasters
                            join cm in DbContext.CompanyMasters on em.CompId equals cm.id
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
                                Status = em.Status

                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("CompanyMaster", "CompanyService", "lstCompany", ex);
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

        public List<CompanyModel> lstCompany()
        {
            try
            {
                var lstcomp = (from comp in DbContext.CompanyMasters
                               select new CompanyModel()
                               {
                                   id = comp.id,
                                   CompCode = comp.CompCode,
                                   CompName = comp.CompName,
                                   ContactPerson = comp.ContactPerson,
                                   Email = comp.Email,
                                   PensionEligibityYears = comp.PensionEligibityYears,
                                   Type = comp.Type,
                                   WidowPension = comp.WidowPension,
                                   Address = comp.Address
                               }).ToList();

                return lstcomp;
            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "EmployeeController", "lstRetirement", ex);
                return null;
            }


        }

        public EmployeeViewModel getEmpbyID(int id)
        {
            try
            {
                Mapper.CreateMap<EmployeeMaster, EmployeeViewModel>();
                EmployeeMaster objmaster = DbContext.EmployeeMasters.SingleOrDefault(m => m.EmpId == id);
                EmployeeViewModel objmasterp = Mapper.Map<EmployeeViewModel>(objmaster);
                return objmasterp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
