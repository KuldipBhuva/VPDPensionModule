using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PensionModel.ViewModel;
using PensionServices;
using PensionModel;
using Pension.Models.ViewModel;
using System.IO;

namespace PensionServices
{
    public class LifeCertificateServices
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        LifeCertificate lifemst = new LifeCertificate();

        public List<LifeCertificateViewModel> lstdata(int id, int compid)
        {
            try
            {
                var lstdata = (from val in DbContext.LifeCertificates
                               join val1 in DbContext.EmployeeMasters on val.EmpId equals val1.EmpId
                               where val.EmpId == id && val.InsCompId == compid
                               select new LifeCertificateViewModel
                               {
                                   //CompanyName = val1.CompName,
                                   id = val.id,
                                   InsCompId = val.InsCompId,
                                   EmpName = val1.EmpName,
                                   YearCode = val.YearCode,
                                   CertificateCopy = val.CertificateCopy,
                                   EffDate = val.EffDate,
                                   Status = val.Status,
                                   ToDate = val.ToDate
                               }).ToList();
                return lstdata;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("n", "n", "LifeCertificate", ex);
                return null;
            }
        }

        public LifeCertificateViewModel CheckExists(string year, int empno)
        {
            try
            {
                var yearData = (from comp in DbContext.LifeCertificates
                                where comp.YearCode == year && comp.EmpId == empno
                                select new LifeCertificateViewModel()
                                {
                                    YearCode = comp.YearCode
                                }).SingleOrDefault();

                return yearData;
            }
            catch (Exception ex)
            {
                ErrorLogClass.WriteErrorLog("AdminManagement", "LifeCertificatesController", "CheckExists", ex);
                return null;
            }
        }

        public int insert(LifeCertificateViewModel model, int flag)
        {
            if (flag == 1)
            {
                lifemst.InsCompId = model.InsCompId;
                lifemst.EmpId = model.EmpId;
                lifemst.YearCode = model.YearCode;
                lifemst.CertificateCopy = model.CertificateCopy;
                lifemst.EffDate = model.EffDate;
                lifemst.Status = model.Status;
                lifemst.ToDate = model.ToDate;

                DbContext.LifeCertificates.Add(lifemst);
                DbContext.SaveChanges();
                return lifemst.id;
            }
            else
            {
                lifemst = DbContext.LifeCertificates.FirstOrDefault(m => m.id == model.hdnlife);
                lifemst.InsCompId = model.InsCompId;
                lifemst.EmpId = model.EmpId;
                lifemst.YearCode = model.YearCode;
                lifemst.CertificateCopy = model.CertificateCopy;
                lifemst.EffDate = model.EffDate;
                lifemst.Status = model.Status;
                lifemst.ToDate = model.ToDate;
                return DbContext.SaveChanges();
            }
        }

        public LifeCertificateViewModel GetByID(int id)
        {
            var lstdata = (from val in DbContext.LifeCertificates
                           where val.id == id
                           select new LifeCertificateViewModel
                           {
                               InsCompId = val.InsCompId,
                               EmpId = val.EmpId,
                               YearCode = val.YearCode,
                               CertificateCopy = val.CertificateCopy,
                               EffDate = val.EffDate,
                               Status = val.Status,
                               ToDate = val.ToDate
                           }).FirstOrDefault();
            return lstdata;
        }
    }
}
