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
    
    public partial class PensionMaster
    {
        public int PID { get; set; }
        public string EmpNo { get; set; }
        public string PolicyNo { get; set; }
        public string AnnuityNo { get; set; }
        public Nullable<decimal> GrossAmt { get; set; }
        public Nullable<decimal> ITAmt { get; set; }
        public Nullable<decimal> CheqAmt { get; set; }
        public string PensionMonth { get; set; }
        public string Year { get; set; }
        public Nullable<decimal> VPDCheqAmt { get; set; }
        public Nullable<decimal> OtherCharge { get; set; }
        public Nullable<System.DateTime> ProcessDate { get; set; }
        public bool IsProcessed { get; set; }
        public Nullable<System.DateTime> UploadedDate { get; set; }
        public Nullable<int> UploadedBy { get; set; }
        public Nullable<System.DateTime> SettelmentDate { get; set; }
    }
}
