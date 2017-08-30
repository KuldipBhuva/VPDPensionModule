using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pension.Models.ViewModel;

namespace PensionModel.ViewModel
{
    public class EmployeePlanViewModel
    {
        public long Id { get; set; }
        public Nullable<long> EmpId { get; set; }
        public Nullable<long> PensionId { get; set; }
        public Nullable<long> LicId { get; set; }
        public Nullable<long> PlanId { get; set; }
        public string AnnuityNo { get; set; }
        public string InsuranceOffife { get; set; }
        public string Email { get; set; }
        public string PolicyId { get; set; }
        public string Plan { get; set; }
        public string Insurance { get; set; }
        public Nullable<System.DateTime> PolicyDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string PolicyStatus { get; set; }
        public Nullable<System.DateTime> HoldDate { get; set; }
        public string Remark { get; set; }
        public Nullable<int> RPensionType { get; set; }
    
        public List<PlanViewModel> ListPlan { get; set; }
        public List<PensionMasterModel> ListPen { get; set; }
        public List<InsuranceViewModel> ListIns { get; set; }

        public List<EmployeePlanViewModel> lstempplan { get; set; }
        public long hdnempplan { get; set; }




    }
}
