using Pension.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class PensionMasterModel
    {
        public int? ptid { get; set; }
        public int PID { get; set; }
        public string EmpNo { get; set; }
        public string PolicyNo { get; set; }
        public string AnnuityNo { get; set; }
        public Nullable<decimal> GrossAmt { get; set; }
        public Nullable<decimal> ITAmt { get; set; }
        public Nullable<decimal> CheqAmt { get; set; }

        [Required(ErrorMessage = "Month Required")]
        public string PensionMonth { get; set; }

        [Required(ErrorMessage = "Year Required")]
        public string Year { get; set; }
        public Nullable<decimal> VPDCheqAmt { get; set; }
        public Nullable<decimal> OtherCharge { get; set; }
        public Nullable<System.DateTime> ProcessDate { get; set; }
        public bool IsProcessed { get; set; }
        public Nullable<System.DateTime> UploadedDate { get; set; }
        public Nullable<int> UploadedBy { get; set; }
        public Nullable<System.DateTime> SettelmentDate { get; set; }
                
        public List<PensionMasterModel> ListPension { get; set; }
        public CompanyModel CompDetails { get; set; }
        public EmployeeViewModel EmpDetails { get; set; }
        public PaymentTypeModel PayTypeDetails { get; set; }
        public LifeCertificateViewModel LifeCertiDetails { get; set; }
        public List<PaymentTypeModel> ListPayType { get; set; }

    }
}
