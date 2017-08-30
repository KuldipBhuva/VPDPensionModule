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
    public class PensionMasterService
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        public List<PensionMasterModel> getPension(int cid)
        {
            try
            {
                Mapper.CreateMap<PensionMaster, PensionMasterModel>();
                List<PensionMaster> tblMaster = DbContext.PensionMasters.Where(m => m.Year == null).ToList();
                List<PensionMasterModel> lstmasterdata = Mapper.Map<List<PensionMasterModel>>(tblMaster);
                return lstmasterdata;

                //var data = (from pm in DbContext.PensionMasters
                //            join em in DbContext.EmployeeMasters on pm.EmpNo equals em.EmpNo
                //            where em.CompId == cid &&(pm.PensionMonth==null || pm.Year==null)
                //            select new PensionMasterModel()
                //            {                              
                //                PID=pm.PID,
                //                PensionMonth=pm.PensionMonth,
                //                Year=pm.Year
                //            }).ToList();
                //return data;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("PensionMaster", "PensionMAsterService", "getPension", ex);
                return null;
            }
        }

        public List<PensionMasterModel> getPensionByANo(string id,string mm,string yy)
        {
            try
            {
                Mapper.CreateMap<PensionMaster, PensionMasterModel>();
                List<PensionMaster> tblMaster = DbContext.PensionMasters.Where(m => (m.PensionMonth!=mm || m.Year!=yy) && m.AnnuityNo==id && m.SettelmentDate==null && m.ProcessDate==null).ToList();
                List<PensionMasterModel> lstmasterdata = Mapper.Map<List<PensionMasterModel>>(tblMaster);
                return lstmasterdata;

                //var data = (from pm in DbContext.PensionMasters
                //            join em in DbContext.EmployeeMasters on pm.EmpNo equals em.EmpNo
                //            where em.CompId == cid &&(pm.PensionMonth==null || pm.Year==null)
                //            select new PensionMasterModel()
                //            {                              
                //                PID=pm.PID,
                //                PensionMonth=pm.PensionMonth,
                //                Year=pm.Year
                //            }).ToList();
                //return data;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("PensionMaster", "PensionMAsterService", "getPension", ex);
                return null;
            }
        }

        public int Update(string year, string month, int pid, int uid)
        {
            try
            {
                PensionMaster objAllo = new PensionMaster();
                objAllo = DbContext.PensionMasters.SingleOrDefault(m => m.PID == pid);
                objAllo.Year = year;
                objAllo.PensionMonth = month;
                objAllo.IsProcessed = false;                
                objAllo.UploadedBy = uid;
                objAllo.UploadedDate = System.DateTime.Now;
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "PensionMasterService", "Update", ex);
                return 0;
            }
        }

        public List<PensionMasterModel> getPensionByComp(int cid)
        {
            try
            {
                var data = (from pm in DbContext.PensionMasters
                            join ep in DbContext.EmployeePlans on pm.AnnuityNo equals ep.AnnuityNo
                            join em in DbContext.EmployeeMasters on ep.EmpId equals em.EmpId
                            join cm in DbContext.CompanyMasters on em.CompId equals cm.id
                            join pt in DbContext.PaymentTypes on em.PaymentType equals pt.id
                            where em.CompId == cid
                            select new PensionMasterModel()
                            {
                                PID = pm.PID,
                                EmpNo = pm.EmpNo,
                                PolicyNo = pm.PolicyNo,
                                AnnuityNo = pm.AnnuityNo,
                                GrossAmt = pm.GrossAmt,
                                ITAmt = pm.ITAmt,
                                CheqAmt = pm.CheqAmt,
                                PensionMonth = pm.PensionMonth,
                                Year = pm.Year,
                                UploadedBy = pm.UploadedBy,
                                UploadedDate = pm.UploadedDate,
                                EmpDetails = new EmployeeViewModel()
                                {
                                    EmpName = em.EmpName,
                                    EmpNo = em.EmpNo
                                },
                                CompDetails = new CompanyModel()
                                {
                                    CompName = cm.CompName
                                },
                                PayTypeDetails = new PaymentTypeModel()
                                {
                                    PaymentType1 = pt.PaymentType1,
                                    OtherCharge = pt.OtherCharge
                                }
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("PensionMaster", "PensionMasterService", "ListPensionByComp", ex);
                return null;
            }
        }

        public List<PensionMasterModel> getPensionByMonth(string year, string month, int cid, int ptid)
        {
            try
            {
                int yr = Convert.ToInt32(year);
                var data = (from pm in DbContext.PensionMasters
                            join ep in DbContext.EmployeePlans on pm.AnnuityNo equals ep.AnnuityNo
                            join em in DbContext.EmployeeMasters on ep.EmpId equals em.EmpId
                            join cm in DbContext.CompanyMasters on em.CompId equals cm.id
                            join pt in DbContext.PaymentTypes on em.PaymentType equals pt.id
                            join lc in DbContext.LifeCertificates on em.EmpId equals lc.EmpId into life
                            from lyf in life.DefaultIfEmpty()
                            //em.PensionStatus == "2" && em.DeathDate == null &&
                            where pm.Year == year && pm.PensionMonth == month && em.CompId == cid && em.PaymentType == (ptid == 0 ? em.PaymentType : ptid) && (lyf.ToDate.Value.Year==yr || lyf.ToDate==null)
                            select new PensionMasterModel()
                            {
                                PID = pm.PID,
                                EmpNo = pm.EmpNo,
                                PolicyNo = pm.PolicyNo,
                                AnnuityNo = pm.AnnuityNo,
                                GrossAmt = pm.GrossAmt,
                                ITAmt = pm.ITAmt,
                                CheqAmt = pm.CheqAmt,
                                PensionMonth = pm.PensionMonth,
                                Year = pm.Year,
                                UploadedBy = pm.UploadedBy,
                                UploadedDate = pm.UploadedDate,
                                IsProcessed = pm.IsProcessed,
                                EmpDetails = new EmployeeViewModel()
                                {
                                    EmpId=em.EmpId,
                                    EmpName = em.EmpName,
                                    EmpNo = em.EmpNo,
                                    DeathDate=em.DeathDate,
                                    PensionStatus=em.PensionStatus
                                },
                                CompDetails = new CompanyModel()
                                {
                                    CompName = cm.CompName
                                },
                                PayTypeDetails = new PaymentTypeModel()
                                {
                                    PaymentType1 = pt.PaymentType1,
                                    OtherCharge = pt.OtherCharge
                                },
                                LifeCertiDetails = new LifeCertificateViewModel()
                                {
                                    ToDate = (lyf == null ? DateTime.MinValue : lyf.ToDate)
                                }
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("PensionMaster", "PensionMasterService", "ListPensionByMonth", ex);
                return null;
            }
        }

        public List<PaymentTypeModel> getActivePayType()
        {
            try
            {
                Mapper.CreateMap<PaymentType, PaymentTypeModel>();
                List<PaymentType> tblMaster = DbContext.PaymentTypes.Where(m => m.Status == 1).ToList();
                List<PaymentTypeModel> lstmasterdata = Mapper.Map<List<PaymentTypeModel>>(tblMaster);
                return lstmasterdata;

                //var data = (from pm in DbContext.PensionMasters
                //            join em in DbContext.EmployeeMasters on pm.EmpNo equals em.EmpNo
                //            where em.CompId == cid &&(pm.PensionMonth==null || pm.Year==null)
                //            select new PensionMasterModel()
                //            {                              
                //                PID=pm.PID,
                //                PensionMonth=pm.PensionMonth,
                //                Year=pm.Year
                //            }).ToList();
                //return data;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("PensionMaster", "PensionMasterService", "getActivePayType", ex);
                return null;
            }
        }
    }
}
