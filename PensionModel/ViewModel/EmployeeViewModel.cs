using PensionModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PensionModel;

namespace Pension.Models.ViewModel
{
    public class EmployeeViewModel
    {
        public int EmpId { get; set; }
        public Nullable<int> CompId { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<System.DateTime> DOJ { get; set; }
        public Nullable<int> RetireTypeId { get; set; }
        public Nullable<System.DateTime> RetireDate { get; set; }
        public Nullable<System.DateTime> DeathDate { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public Nullable<int> FHStatus { get; set; }
        public string FHName { get; set; }
        public string Relation { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string ContactO { get; set; }
        public string ContactR { get; set; }
        public string Email { get; set; }
        public string AadharNo { get; set; }
        public string PANNo { get; set; }
        public string BankAcNo { get; set; }
        public string BankName { get; set; }
        public string AccType { get; set; }
        public string IFSC { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string PensionStatus { get; set; }
        public string Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> PentionTypeID { get; set; }
        public Nullable<long> LicId { get; set; }
        public Nullable<int> PaymentType { get; set; }
        public string EmployeementType { get; set; }
        public Nullable<long> GradeId { get; set; }
        public Nullable<int> EmployeeType { get; set; }
        public Nullable<System.DateTime> HoldDate { get; set; }
        public string Remarks { get; set; }
        public string AnnuityMode { get; set; }
        public Nullable<int> Merged { get; set; }
        public Nullable<int> FromMerger { get; set; }
        public Nullable<System.DateTime> MDOJ { get; set; }
        public Nullable<System.DateTime> MDOL { get; set; }
        public Nullable<decimal> FrozenAmt { get; set; }
        public Nullable<decimal> FrozenPensionAmt { get; set; }
        public Nullable<int> Benefits { get; set; }
        public string Contribution { get; set; }
        public Nullable<decimal> RPensionAmt { get; set; }
        public Nullable<System.DateTime> SASettleDate { get; set; }


        public string GradeName { get; set; }
        public List<CompanyModel> lstCmy { get; set; }
        List<CompanyModel> lstCompany = new List<CompanyModel>();
        public CompanyModel CompDetails { get; set; }
        public int hdnEmpId { get; set; }
        public DateTime DueDate { get; set; }
        public List<EmployeeViewModel> ListEmp { get; set; }
        public List<RetirementTypeViewModel> lstRetirement { get; set; }
        public List<GradeViewModel> Listgrade { get; set; }
        public List<PaymentTypeModel> Listpaytype { get; set; }
        public List<PlanViewModel> Listpen { get; set; }
        public List<PaymentType> Listpay { get; set; }
        public List<InsuranceViewModel> ListIns { get; set; }
    }
}