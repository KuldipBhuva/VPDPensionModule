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
    
    public partial class PensionLimit
    {
        public long Id { get; set; }
        public Nullable<long> CmyID { get; set; }
        public string EmployeementType { get; set; }
        public Nullable<long> GradeID { get; set; }
        public Nullable<decimal> Pension_Limit { get; set; }
        public Nullable<System.DateTime> EffDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> Status { get; set; }
    }
}