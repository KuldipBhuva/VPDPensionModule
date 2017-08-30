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

    public interface ICompanyServices
    {
        List<CompanyModel> lstCompany();
        int InsertCompany(CompanyModel model, PensionLimit penmodel, MergingCompany mergemodel, SAInterest samodel);
        CompanyModel GetById(int compid);
        int UpdateCompanyData(int Id, int said, CompanyModel model, PensionLimit penmodel, MergingCompany mergemodel, SAInterest samodel);
        CompanyModel CheckExists(string CompName);
    }


    public class CompanyServices : ICompanyServices, IDisposable
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
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
                                   Address = comp.Address,
                                   SAIntMethod = comp.SAIntMethod,
                                   SAStopIntType = comp.SAStopIntType,
                                   SAContriRate = comp.SAContriRate,
                                   SAContriMethod = comp.SAContriMethod
                               }).ToList();

                return lstcomp;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("CompanyMaster", "CompanyService", "lstCompany", ex);
                return null;
            }
        }

        public List<CompanyModel> lstCmy(int cmy)
        {
            try
            {
                var lstcmy = (from comp in DbContext.CompanyMasters
                              where !(from b in DbContext.MergingCompanies
                                      where b.CmyID == cmy
                                      select b.MCmyID)
                                      .Contains(comp.id) &&
                                      !(from b in DbContext.MergingCompanies
                                        where b.CmyID == cmy
                                        select b.CmyID)
                                      .Contains(comp.id)
                              select new CompanyModel()
                              {
                                  id = comp.id,
                                  CompCode = comp.CompCode,
                                  CompName = comp.CompName
                              }).ToList();
                return lstcmy;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("CompanyMaster", "CompanyService", "lstCompany", ex);
                return null;
            }
        }

        public List<CompanyModel> lstMergeCmy(int cmy)
        {
            try
            {
                var lstcmy = (from mcmy in DbContext.MergingCompanies
                              join comp in DbContext.CompanyMasters on mcmy.MCmyID equals comp.id
                              where mcmy.CmyID == cmy
                              select new CompanyModel()
                              {
                                  id = comp.id,
                                  CompCode = comp.CompCode,
                                  CompName = comp.CompName
                              }).ToList();
                return lstcmy;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("CompanyMaster", "CompanyService", "lstCompany", ex);
                return null;
            }
        }

        public int InsertCompany(CompanyModel model, PensionLimit penmodel, MergingCompany mergemodel, SAInterest samodel)
        {
            try
            {
                CompanyMaster objcomp = new CompanyMaster();
                PensionLimit penlimit = new PensionLimit();
                MergingCompany Mergecomp = new MergingCompany();
                objcomp.CompCode = model.CompCode;
                objcomp.CompName = model.CompName;
                objcomp.ContactPerson = model.ContactPerson;
                objcomp.Email = model.Email;
                objcomp.Address = model.Address;
                objcomp.WidowPension = model.WidowPension;
                objcomp.PensionEligibityYears = model.PensionEligibityYears;
                objcomp.Status = 1;
                objcomp.PensionLimit = model.PensionLimit;
                objcomp.Merging = model.Merging;
                objcomp.SAIntMethod = model.SAIntMethod;
                objcomp.SAStopIntType = model.SAStopIntType;
                objcomp.SAContriRate = model.SAContriRate;
                objcomp.SAContriMethod = model.SAContriMethod;
                DbContext.CompanyMasters.Add(objcomp);
                DbContext.SaveChanges();
                if (model.PensionLimit == "Yes")
                {
                    penlimit.CmyID = objcomp.id;
                    penlimit.EmployeementType = penmodel.EmployeementType;
                    penlimit.GradeID = penmodel.GradeID;
                    penlimit.Pension_Limit = penmodel.Pension_Limit;
                    penlimit.EffDate = penmodel.EffDate;
                    penlimit.CreateDate = System.DateTime.Now;
                    penlimit.Status = 1;
                    DbContext.PensionLimits.Add(penlimit);
                    DbContext.SaveChanges();
                }
                if (model.Merging == "Yes")
                {
                    Mergecomp.CmyID = objcomp.id;
                    Mergecomp.MCmyID = mergemodel.MCmyID;
                    Mergecomp.EffDate = mergemodel.EffDate;
                    Mergecomp.CreateDate = System.DateTime.Now;
                    Mergecomp.Status = 1;
                    DbContext.MergingCompanies.Add(Mergecomp);
                    DbContext.SaveChanges();
                }
                if (model.LiveEmpInt != 0 && model.LeftEmpInt != 0)
                {
                    SAInterest saint = new SAInterest();
                    saint.CompID = objcomp.id;
                    saint.LiveEmpInt = samodel.LiveEmpInt;
                    saint.LiveEffYear = samodel.LiveEffYear;
                    saint.LeftEmpInt = samodel.LeftEmpInt;
                    saint.LeftEffYear = samodel.LeftEffYear;
                    DbContext.SAInterests.Add(saint);
                    DbContext.SaveChanges();
                }
                return objcomp.id;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("CompanyMaster", "CompanyService", "InsertCompany", ex);
                return 0;
            }
        }

        public CompanyModel GetById(int compid)
        {
            try
            {
                var lstcomp = (from comp in DbContext.CompanyMasters
                               where comp.id == compid
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
                                   Address = comp.Address,
                                   Status = comp.Status,
                                   PensionLimit = comp.PensionLimit,
                                   Merging = comp.Merging,
                                   SAIntMethod = comp.SAIntMethod,
                                   SAStopIntType = comp.SAStopIntType,
                                   SAContriRate = comp.SAContriRate,
                                   SAContriMethod = comp.SAContriMethod
                               }).FirstOrDefault();
                return lstcomp;
            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyService", "GetById", ex);
                return null;
            }
        }

        public List<CompanyModel> GetPenByCmyId(int cmyid)
        {
            try
            {
                var lstpension = (from comp in DbContext.CompanyMasters
                                  join pen in DbContext.PensionLimits on comp.id equals pen.CmyID
                                  join grade in DbContext.GradeMasters on pen.GradeID equals grade.id
                                  where pen.CmyID == cmyid
                                  select new CompanyModel()
                                  {
                                      PenId = pen.Id,
                                      CmyID = pen.CmyID,
                                      EmployeementType = pen.EmployeementType,
                                      GradeID = pen.GradeID,
                                      GradeName = grade.Grade_Name,
                                      Pension_Limit = pen.Pension_Limit,
                                      PenEffDate = pen.EffDate,
                                      CreateDate = pen.CreateDate,
                                      Status = pen.Status,
                                      CompName = comp.CompName
                                  }).ToList();
                return lstpension;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyService", "GetById", ex);
                return null;
            }
        }

        public List<CompanyModel> GetMergerByCmyId(int cmpid)
        {
            try
            {
                var lstmerger = (from comp in DbContext.CompanyMasters
                                 join merge in DbContext.MergingCompanies on comp.id equals merge.MCmyID
                                 where merge.CmyID == cmpid
                                 select new CompanyModel()
                                 {
                                     MerId = merge.Id,
                                     CmyID = merge.CmyID,
                                     MCmyID = merge.MCmyID,
                                     MergeEffDate = merge.EffDate,
                                     CreateDate = merge.CreateDate,
                                     Status = merge.Status,
                                     CompName = comp.CompName
                                 }).ToList();
                return lstmerger;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyService", "GetById", ex);
                return null;
            }
        }

        public CompanyModel GetPenById(int cmyid, int penid)
        {
            try
            {
                var lstpension = (from comp in DbContext.CompanyMasters
                                  join pen in DbContext.PensionLimits on comp.id equals pen.CmyID
                                  join grade in DbContext.GradeMasters on pen.GradeID equals grade.id
                                  where pen.CmyID == cmyid && pen.Id == penid
                                  select new CompanyModel()
                                  {
                                      id = comp.id,
                                      CompCode = comp.CompCode,
                                      CompName = comp.CompName,
                                      ContactPerson = comp.ContactPerson,
                                      GradeName = grade.Grade_Name,
                                      Email = comp.Email,
                                      PensionEligibityYears = comp.PensionEligibityYears,
                                      Type = comp.Type,
                                      WidowPension = comp.WidowPension,
                                      Address = comp.Address,
                                      Status = comp.Status,
                                      PensionLimit = comp.PensionLimit,
                                      Merging = comp.Merging,

                                      PenId = pen.Id,
                                      CmyID = pen.CmyID,
                                      EmployeementType = pen.EmployeementType,
                                      GradeID = pen.GradeID,
                                      Pension_Limit = pen.Pension_Limit,
                                      PenEffDate = pen.EffDate,
                                      CreateDate = pen.CreateDate
                                      //,Status = pen.Status
                                  }).FirstOrDefault();
                return lstpension;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyService", "GetById", ex);
                return null;
            }
        }

        public CompanyModel GetMergerById(int cmpid, int merid)
        {
            try
            {
                var lstmerger = (from comp in DbContext.CompanyMasters
                                 join merge in DbContext.MergingCompanies on comp.id equals merge.MCmyID
                                 where merge.Id == cmpid && merge.Id == merid
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
                                     Address = comp.Address,
                                     Status = comp.Status,
                                     PensionLimit = comp.PensionLimit,
                                     Merging = comp.Merging,

                                     MerId = merge.Id,
                                     CmyID = merge.CmyID,
                                     MCmyID = merge.MCmyID,
                                     MergeEffDate = merge.EffDate,
                                     CreateDate = merge.CreateDate
                                     //,Status = merge.Status
                                 }).FirstOrDefault();
                return lstmerger;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyService", "GetById", ex);
                return null;
            }
        }

        public int UpdateCompanyData(int Id, int Said, CompanyModel model, PensionLimit penmodel, MergingCompany mergemodel, SAInterest samodel)
        {
            try
            {
                CompanyMaster objcomp = new CompanyMaster();
                PensionLimit penlimit = new PensionLimit();
                MergingCompany Mergecomp = new MergingCompany();
                SAInterest saint = new SAInterest();
                objcomp = DbContext.CompanyMasters.SingleOrDefault(m => m.id == Id);
                objcomp.CompCode = model.CompCode;
                objcomp.CompName = model.CompName;
                objcomp.ContactPerson = model.ContactPerson;
                objcomp.Type = model.Type;
                objcomp.Email = model.Email;
                objcomp.Address = model.Address;
                objcomp.WidowPension = model.WidowPension;
                objcomp.PensionEligibityYears = model.PensionEligibityYears;
                objcomp.Status = 1;
                objcomp.PensionLimit = model.PensionLimit;
                objcomp.Merging = model.Merging;
                objcomp.SAIntMethod = model.SAIntMethod;
                objcomp.SAStopIntType = model.SAStopIntType;
                objcomp.SAContriRate = model.SAContriRate;
                objcomp.SAContriMethod = model.SAContriMethod;
                if (penmodel.Pension_Limit != null && penmodel.EmployeementType != null && penmodel.GradeID != null && penmodel.EffDate != null)
                {
                    penlimit = DbContext.PensionLimits.SingleOrDefault(m => m.CmyID == Id);
                    if (penlimit == null)
                    {
                        penlimit = new PensionLimit();
                        penlimit.CmyID = objcomp.id;
                        penlimit.EmployeementType = penmodel.EmployeementType;
                        penlimit.GradeID = penmodel.GradeID;
                        penlimit.Pension_Limit = penmodel.Pension_Limit;
                        penlimit.EffDate = penmodel.EffDate;
                        DbContext.PensionLimits.Add(penlimit);
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        penlimit.EmployeementType = penmodel.EmployeementType;
                        penlimit.GradeID = penmodel.GradeID;
                        penlimit.Pension_Limit = penmodel.Pension_Limit;
                        penlimit.EffDate = penmodel.EffDate;
                        DbContext.SaveChanges();
                    }
                }
                if (mergemodel.EffDate != null && mergemodel.MCmyID != null)
                {
                    Mergecomp = DbContext.MergingCompanies.SingleOrDefault(m => m.CmyID == Id);
                    if (Mergecomp == null)
                    {
                        Mergecomp = new MergingCompany();
                        Mergecomp.CmyID = Id;
                        Mergecomp.MCmyID = mergemodel.MCmyID;
                        Mergecomp.EffDate = mergemodel.EffDate;
                        DbContext.MergingCompanies.Add(Mergecomp);
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        Mergecomp.MCmyID = mergemodel.MCmyID;
                        Mergecomp.EffDate = mergemodel.EffDate;
                        DbContext.SaveChanges();
                    }
                }
                if (model.LiveEmpInt != 0 && model.LeftEmpInt != 0)
                {
                    if (Said != null)
                    {
                        saint = DbContext.SAInterests.Where(m => m.ID == Said).FirstOrDefault();
                        saint.LiveEmpInt = samodel.LiveEmpInt;
                        saint.LiveEffYear = samodel.LiveEffYear;
                        saint.LeftEmpInt = samodel.LeftEmpInt;
                        saint.LeftEffYear = samodel.LeftEffYear;
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        saint = new SAInterest();
                        saint.CompID = Id;
                        saint.LiveEmpInt = samodel.LiveEmpInt;
                        saint.LiveEffYear = samodel.LiveEffYear;
                        saint.LeftEmpInt = samodel.LeftEmpInt;
                        saint.LeftEffYear = samodel.LeftEffYear;
                        DbContext.SAInterests.Add(saint);
                        DbContext.SaveChanges();
                    }
                }
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyService", "UpdateCompanyData", ex);
                return 0;
            }
        }

        public CompanyModel CheckExists(string CompName)
        {
            try
            {
                var CompData = (from comp in DbContext.CompanyMasters
                                where comp.CompName == CompName
                                select new CompanyModel()
                                {
                                    CompName = comp.CompName
                                }).SingleOrDefault();

                return CompData;
            }

            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "CompanyService", "CheckExists", ex);
                return null;
            }
        }

        public List<CompanyModel> lstSAInt(int cmy)
        {
            try
            {
                var lstsa = (from sa in DbContext.SAInterests
                             where sa.CompID == cmy
                             select new CompanyModel()
                             {
                                 SaID = sa.ID,
                                 LiveEffYear = sa.LiveEffYear,
                                 LiveEmpInt = sa.LiveEmpInt,
                                 LeftEffYear = sa.LeftEffYear,
                                 LeftEmpInt = sa.LeftEmpInt
                             }).ToList();
                return lstsa;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("CompanyMaster", "CompanyService", "lstCompany", ex);
                return null;
            }
        }

        public CompanyModel GetSaById(int cmy, int id)
        {
            try
            {
                var lstsa = (from comp in DbContext.CompanyMasters
                             join sa in DbContext.SAInterests on comp.id equals sa.CompID
                             where sa.ID == id
                             select new CompanyModel()
                             {
                                 LiveEffYear = sa.LiveEffYear,
                                 LiveEmpInt = sa.LiveEmpInt,
                                 LeftEffYear = sa.LeftEffYear,
                                 LeftEmpInt = sa.LeftEmpInt
                             }).FirstOrDefault();
                return lstsa;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("CompanyMaster", "CompanyService", "lstCompany", ex);
                return null;
            }
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
