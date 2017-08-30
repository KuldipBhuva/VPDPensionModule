using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionModel.ViewModel
{
    public class LifeCertificateViewModel
    {
        public int id { get; set; }
        public Nullable<int> InsCompId { get; set; }
        public string EmpName { get; set; }
        public Nullable<int> EmpId { get; set; }
        public string YearCode { get; set; }
        public string CertificateCopy { get; set; }
        public Nullable<System.DateTime> EffDate { get; set; }
        public Nullable<int> Status { get; set; }
        public string Value { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }

        public List<LifeCertificateViewModel> lstlife { get; set; }
        public int hdnlife { get; set; }
        public string hdnimg { get; set; }
    }
}
