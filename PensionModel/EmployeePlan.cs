//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PensionModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeePlan
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
        public Nullable<System.DateTime> PolicyDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string PolicyStatus { get; set; }
        public Nullable<System.DateTime> HoldDate { get; set; }
        public string Remark { get; set; }
        public Nullable<int> RPensionType { get; set; }
    }
}