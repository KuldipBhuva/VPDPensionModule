using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class ReportViewModel
    {
        public Nullable<long> EmpId { get; set; }
        public string EmpName { get; set; }
        public string Status { get; set; }

        //Monthly Employee List
        public Nullable<System.DateTime> LCtill { get; set; }
        public Nullable<System.DateTime> RetDate { get; set; }
        public string RetType { get; set; }
        public List<ReportViewModel> lstemp { get; set; }


        //PolicyReport
        public string Insurance { get; set; }
        public string PolicyId { get; set; }
        public string AnnuityNo { get; set; }
        public string AnnuityName { get; set; }
        public Nullable<decimal> AnnuityAmt { get; set; }
        public string Arrears { get; set; }
        public string PlanName { get; set; }
        public string Remarks { get; set; }
        public string HRemarks { get; set; }
        public List<ReportViewModel> lstempplan { get; set; }

        //SalaryMaster
        public string EmpNo { get; set; }
        public Nullable<decimal> Basic { get; set; }
        public Nullable<decimal> HRA { get; set; }
        public Nullable<decimal> DA { get; set; }
        public Nullable<decimal> EA { get; set; }
        public Nullable<decimal> Conveyance { get; set; }
        public Nullable<decimal> Overtime { get; set; }
        public Nullable<decimal> PF { get; set; }
        public Nullable<decimal> Medical { get; set; }
        public Nullable<int> CompID { get; set; }
        public Nullable<int> SalaryMonth { get; set; }
        public string Year { get; set; }
        public Nullable<int> UploadedBy { get; set; }
        public Nullable<System.DateTime> UploadedDate { get; set; }

        public List<ReportViewModel> lstSalary { get; set; }
        public List<SAContributionModel> ListSA { get; set; }
    }
}
