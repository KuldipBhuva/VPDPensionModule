using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PensionModel;
using PensionModel.ViewModel;

namespace PensionServices
{
    public class ReportServices
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();

        public List<Dictionary<string, object>> Read(DbDataReader reader)
        {
            List<Dictionary<string, object>> expandolist = new List<Dictionary<string, object>>();
            foreach (var item in reader)
            {
                IDictionary<string, object> expando = new ExpandoObject();
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(item))
                {
                    var obj = propertyDescriptor.GetValue(item);
                    expando.Add(propertyDescriptor.Name, obj);
                }
                expandolist.Add(new Dictionary<string, object>(expando));
            }
            return expandolist;
        }

        public List<ReportViewModel> lstsalarydata(int cmy, int emp)
        {
            var lstsalarydata = (from val in DbContext.SalaryMasters
                                 join val1 in DbContext.EmployeeMasters on val.EmpNo equals val1.EmpNo
                                 where val.CompID == cmy && val1.EmpId == emp
                                 select new ReportViewModel()
                                 {
                                     SalaryMonth = val.SalaryMonth,
                                     Basic = val.Basic,
                                     HRA = val.HRA,
                                     DA = val.DA,
                                     EA = val.EA,
                                     Conveyance = val.Conveyance,
                                     Medical = val.Medical,
                                     Overtime = val.Overtime
                                 }).ToList();
            return lstsalarydata;
        }

        public List<ReportViewModel> lstpolicydata(int comp)
        {
            var lstpolicydata = (from val in DbContext.EmployeeMasters
                                 join val1 in DbContext.EmployeePlans on val.EmpId equals val1.EmpId
                                 join val2 in DbContext.InsuranceMasters on val1.LicId equals val2.id
                                 join val3 in DbContext.PlanMasters on val1.PlanId equals val3.id
                                 where val.CompId == comp
                                 select new ReportViewModel()
                                 {
                                     EmpId = val1.EmpId,
                                     EmpName = val.EmpName,
                                     Insurance = val2.InsuranceCompany,
                                     PolicyId = val1.PolicyId,
                                     AnnuityNo = val1.AnnuityNo,
                                     AnnuityName = val1.InsuranceOffife,
                                     AnnuityAmt = val1.Amount,
                                     //Arrears = val.Arrears,
                                     PlanName = val3.PlanName,
                                     HRemarks = val1.Remark,
                                     Status = val1.PolicyStatus
                                 }).ToList();
            return lstpolicydata;
        }

        public List<ReportViewModel> lstempdata(int comp)
        {
            var lstempdata = (from val in DbContext.EmployeeMasters
                              join val2 in DbContext.LifeCertificates on val.EmpId equals val2.EmpId
                              join val3 in DbContext.RetirementType_Master on val.RetireTypeId equals val3.id
                              where val.CompId == comp
                              select new ReportViewModel()
                              {
                                  EmpId = val.EmpId,
                                  EmpName = val.EmpName,
                                  LCtill = val2.ToDate,
                                  RetDate = val.RetireDate,
                                  RetType = val3.RetirementType,
                                  Status = val.PensionStatus
                              }).ToList();
            return lstempdata;
        }
    }
}
